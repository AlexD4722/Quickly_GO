using Microsoft.IdentityModel.Tokens;
using QuicklyGo.Models;
using QuicklyGo.Reponses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuicklyGo.Utils
{
    public class Auth
    {
        public static string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static ValueClaimTokenResponse? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time 
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var ValueClaimTokenResponse = new ValueClaimTokenResponse
                {
                    UserId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    UserName = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value
                };
                // return user id from JWT token if validation successful
                return ValueClaimTokenResponse;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

    }
}
