    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using CarTrack_API.BusinessLogic.Mapping;
    using CarTrack_API.DataAccess.Repositories.RefreshTokenRepository;
    using CarTrack_API.EntityLayer.Dtos.Auth;
    using CarTrack_API.EntityLayer.Models;
    using Microsoft.IdentityModel.Tokens;

    namespace CarTrack_API.BusinessLogic.Services.JwtService
    {
        public class JwtService(IConfiguration config, IRefreshTokenRepository refreshRepository) : IJwtService
        {
            private readonly IConfiguration _config = config;
            private readonly IRefreshTokenRepository _refreshRepository = refreshRepository;
            
            
            public string GenerateJwtToken(User user)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("username", user.Username),
                    new Claim("role", user.Role.Role), // Assuming Role is a string
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token ID
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            
            public string GenerateRefreshToken()
            {
                var randomBytes = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
            
            public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
            {
                var storedToken = await _refreshRepository.GetByTokenAsync(refreshToken); // Acesta este RT_VECHI

                if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
                {
                    // TODO: Adaugă aici logică de securitate. Dacă cineva încearcă să folosească un RT revocat,
                    // ar trebui invalidate toate RT-urile pentru acel user, ca măsură de precauție (detectare a furtului de RT).
                    throw new UnauthorizedAccessException("Invalid or expired refresh token.");
                }

                var user = storedToken.User;
                if (user == null) // Verificare suplimentară importantă
                {
                    // Acest lucru nu ar trebui să se întâmple dacă GetByTokenAsync include User și token-ul e valid
                    throw new ApplicationException("User not found for a valid refresh token.");
                }


                // Generează tokenuri noi
                var newAccessToken = GenerateJwtToken(user);
                var newRefreshTokenValue = GenerateRefreshToken(); // Valoarea string a noului RT

                // Marchează tokenul VECHI (storedToken) ca revocat
                storedToken.IsRevoked = true;
                await _refreshRepository.UpdateRefreshTokenAsync(storedToken); // Actualizează RT_VECHI în BD (doar flag-ul IsRevoked)

                // Creează noua entitate RefreshToken pentru RT_NOU
                var newRefreshTokenEntity = new RefreshToken // Presupunând că ai un model RefreshToken
                {
                    Token = newRefreshTokenValue,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7), // Sau cât e durata de viață a RT-ului
                    IsRevoked = false
                };
    
                // Adaugă RT_NOU în baza de date
                await _refreshRepository.AddRefreshTokenAsync(newRefreshTokenEntity);

                return new AuthResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshTokenValue // Returnează valoarea string a noului RT
                };
            }
        }
    }