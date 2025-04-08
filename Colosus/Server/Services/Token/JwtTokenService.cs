using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Services.Token
{
    public class JwtTokenService : ITokenService
    {
        IDataConverter converter;
        public JwtTokenService(IDataConverter converter)
        {
            this.converter = converter;
        }

        public string __secretkey = "b2d1e7f97e0a2d5289f8dbcf9412b7ab";
        public int ValidityTime = 120;

        public string GenerateJwtToken(string userId, string name, List<string> roles)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(__secretkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> myClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, converter.Serialize(roles)),
            };

            var tokenDescriptor = new JwtSecurityToken(claims: myClaims, expires: DateTime.Now.AddMinutes(ValidityTime), signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }

        public string GetUserIDFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jsonToken == null)
                return "";

            string userToken = string.Join(",", jsonToken?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString());

            return userToken;
        }

        public string GetUserRolesFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jsonToken == null)
                return "";

            string roles = string.Join(",", jsonToken?.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(xd => xd.Value.ToString().Replace("[", "").Replace("]", "").Replace("\"", "")) ?? Enumerable.Empty<string>());


            return roles;
        }

        public bool ValidateJwtToken(ref string token)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(__secretkey)); // Aynı gizli anahtar
            var tokenHandler = new JwtSecurityTokenHandler();
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim(); 
            }

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false, 
                    ValidateAudience = false, 
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                // Token geçersiz
                Console.WriteLine($"Token doğrulama hatası: {ex.Message}");
                return false;
            }

        }


    }
}
