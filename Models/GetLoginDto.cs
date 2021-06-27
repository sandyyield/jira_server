using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Models
{
    /// <summary>
    /// 登录入参
    /// </summary>
    public class GetLoginDto
    {
        /// <summary>
        ///用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///密码
        /// </summary>
        public string Pass { get; set; }
    }
}
