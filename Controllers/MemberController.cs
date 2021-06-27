using Jira_server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController : BaseController
    {

        [HttpPost("getuserinfo")]
        public async Task<JwtUserInfo> GetUserInfoAsync([FromBody] UserInfo value)
        {
            var user = GetUseByJwtToken();
            //todo...

            return user;
        }

        //[AllowAnonymous]
        [HttpGet("getuserinfo2")]
        
        //public async Task<JwtUserInfo> GetUserInfoAsync2([FromQuery] UserInfo value)
        public async Task<JwtUserInfo> GetUserInfoAsync2()
        {
            Console.WriteLine("debug api");
            var user = GetUseByJwtToken();

            return user;
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "system")]
        [HttpGet("Get"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<string> Get()
        {
            return new string[] { "aaa", "bbbb" };

        }
    }
}
