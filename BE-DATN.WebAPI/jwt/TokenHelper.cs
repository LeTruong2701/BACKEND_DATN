using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BE_DATN.WebAPI.jwt
{
    public static class TokenHelper
    {
        public static string GenerateToken(string jwtSecret, string issuer, string audience
       , IList<string> userRoles, string id, string userName, string email)
        {
            List<Claim> authClaims = new List<Claim>();
            List<Claim> claimRoles = userRoles.Select(s => new Claim(AppJwtClaimTypes.Roles, s)).ToList();

            authClaims.AddRange(new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToLower()),
            new Claim(AppJwtClaimTypes.Subject, id.ToLower()),
            new Claim(AppJwtClaimTypes.UserName, userName),
            new Claim(AppJwtClaimTypes.Email, email)
        });

            authClaims.AddRange(claimRoles);

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));


            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                expires: DateTime.Now.AddDays(7), // Các bạn có thể set thời gian hết hạn của token ở đây
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static DateTime GetValidTo(string jwt)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jwtSecurityToken = handler.ReadJwtToken(jwt);
            return jwtSecurityToken.ValidTo;
        }
    }
}
