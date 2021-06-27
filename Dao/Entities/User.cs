using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jira_server.Dao.Managers
{
    //[Table("user")]
    public class User
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,MaxLength(100) ]
        public string UserName { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        
        public DateTime Createtime { get; set; }

        public string NickName { get; set; }

        public string Mobile { get; set; }

        
    }


}
