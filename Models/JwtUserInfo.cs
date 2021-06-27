using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Models
{
    public class JwtUserInfo
    {
        public string id { get; set; } = "0";

        public string username { get; set; } = "";

        public string Mobile { get; set; } = "";

        public string WxNickName { get; set; } = "";
    }
}
