using MVC_2.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_2.Models
{
    [Table("TB_M_User")]
    public class User : BaseModel
    {
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Role Role_id { get; set; }
    }

    public class take
    {
        public String Email { get; set; }
        public String Password { get; set; }
    }
}