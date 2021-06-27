using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Utils
{
    public class TokenHelper
    {

        //private string BuildToken(string userId)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("Security:Tokens:Key");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Issuer = "Security:Tokens:Issuer",
        //        Audience = "Security:Tokens:Audience",
        //        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userId) }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);

        //}
    }
}
