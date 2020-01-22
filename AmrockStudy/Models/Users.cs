using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmrockStudy.Models
{
    public class Users
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { set; get; }
        public string UserEmail { set; get; }
        public ICollection<Orders> Orders { set; get; }
    }
}
