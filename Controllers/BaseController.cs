using Jira_server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Controllers
{
    
    public class BaseController:ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public JwtUserInfo GetUseByJwtToken()
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
                var clims = new JwtSecurityToken(token).Claims;
                return new JwtUserInfo()
                {
                    id = clims.FirstOrDefault(i => i.Type == nameof(JwtUserInfo.id)).Value ?? "",
                    username = clims.FirstOrDefault(i => i.Type == nameof(JwtUserInfo.username)).Value ?? "",
                    Mobile = clims.FirstOrDefault(i => i.Type == nameof(JwtUserInfo.Mobile)).Value ?? "",
                    WxNickName = clims.FirstOrDefault(i => i.Type == nameof(JwtUserInfo.WxNickName)).Value ?? "",
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
