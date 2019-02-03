using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    class TokenService
    {
        public static string GenerateTokens(string UserName, List<string> Claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SymmetricSecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };
            foreach (var item in Claims)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var tokeOptions = new JwtSecurityToken(
                issuer: Constants.ValidIssuer,
                audience: Constants.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

    }
}
