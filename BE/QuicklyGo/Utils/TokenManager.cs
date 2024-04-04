using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuicklyGo.Utils
{
    public class TokenManager
    {
        public static string CreateToken(string userId, string role)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                                                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                                                    claims: new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId), new Claim(ClaimTypes.Role, role) },
                                                    expires: DateTime.Now.AddHours(12),
                                                    signingCredentials: signinCredentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        public static string CreateRefreshToken(string userId)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                                                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                                                    claims: new List<Claim> { new Claim("userId", userId) },
                                                    expires: DateTime.Now.AddDays(24),
                                                    signingCredentials: signinCredentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        public static bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]);
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    ValidAudience = ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static string GetIdFromToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.First(claim => claim.Type == "userId").Value;
        }
    }
}
