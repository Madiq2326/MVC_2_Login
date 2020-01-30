using MVC_2.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_2.Models
{
    [Table("TB_M_Role")]
    public class Role : BaseModel
    {
        public String Role_Name { get; set; }
    }
}