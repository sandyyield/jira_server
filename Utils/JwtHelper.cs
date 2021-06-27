using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jira_server.Utils
{
    /// <summary>
    /// jwt操作类
    /// </summary>
    public class JwtHelper
    {


        /// <summary>
        /// 创建jwttoken
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        public static string BuildJwtToken(IEnumerable<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.GetConfigToString("JWTSetting:JWTKey")));

            DateTime expiresAt = DateTime.Now.AddDays(ConfigHelper.GetConfigToInt("JWTSetting:JWTExpires"));

            //将用户信息添加到 Claim 中
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            identity.AddClaims(claims);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//创建声明信息
                Issuer = ConfigHelper.GetConfigToString("JWTSetting:JWTIssuer"),//Jwt token 的签发者
                Audience = ConfigHelper.GetConfigToString("JWTSetting:JWTAudience"),//Jwt token 的接收者
                NotBefore = DateTime.Now,
                Expires = expiresAt,//过期时间
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),//创建 token
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public static async bool DelJwtToken(string token)
        //{
        //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //    tokenHandler.ValidateToken(token,)
        //    return tokenHandler.ReadJwtToken(token);

        //}

        /// <summary>
        /// 获取时间戳  13位
        /// </summary>
        /// <param name="dtime">The dtime.</param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dtime)
        {
            TimeSpan ts = dtime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }
    }
}
