using Jira_server.Dao.DbContexts;
using Jira_server.Dao.Managers;
using Jira_server.Models;
using Jira_server.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jira_server.Controllers
{
    /// <summary>
    /// 授权控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("login")]
        public async Task<LoginDto> LoginAsync([FromQuery] UserInfo val)
        {

            var user = _context.Users.Where(i => i.UserName == val.username && i.Password == val.password).SingleOrDefault();

            if (user == default)
            {
                return new LoginDto
                {
                    IsSuccessed = false,
                    Message = "档期那用户未注册"
                };
            }



            //组织JWT 信息
            var claims = new Claim[]
            {
                new Claim(nameof(JwtUserInfo.username),user.UserName),
                new Claim(nameof(JwtUserInfo.WxNickName),user.NickName ?? "..."),
                new Claim(nameof(JwtUserInfo.Mobile),user.Mobile?? "10000"),
                new Claim(nameof(JwtUserInfo.id),user.Id.ToString()), //tofix 随便给个数
                new Claim("guid",Guid.NewGuid().ToString("N")),
            };


            var result = new LoginDto
            {
                IsSuccessed = true,
                Message = "login success!!!",
                Token = JwtHelper.BuildJwtToken(claims),
                Expired = JwtHelper.GetTimeStamp(DateTime.UtcNow.AddDays(ConfigHelper.GetConfigToInt("JWTSetting:JWTExpires"))).ToString(),
            };

            return result;

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInfo val)
        {

            if (_context.Users.Where(i => i.UserName == val.username).Count() > 0)
            {
                return Content("已存在同名用户,不允许注册");
            }

            _context.Add(new User { UserName = val.username, Password = val.password, Createtime = DateTime.UtcNow });
            _context.SaveChanges();


            return Content("注册成功");
        }

        //[HttpPost("logout")]
        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{

        //    JwtHelper.
        //}

    }
}
