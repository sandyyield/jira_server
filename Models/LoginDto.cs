using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Models
{
    public class LoginDto
    {
        //public string username { get; set; }

        //public string password { get; set; }

        public bool IsSuccessed { get; set; }

        public string Token { get; set; }

        public string Expired { get; set; }

        public string Message { get; set; }

    }
}
