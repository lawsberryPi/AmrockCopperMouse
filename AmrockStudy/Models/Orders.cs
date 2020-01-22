using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmrockStudy.Models
{
    // joining entity class which includes the foreign key perperty 
    // and the reference navigation perperty for each entity 
    public class Orders
    {
        public string id { get; set; }
        public GeneralProduct GeneralProduct { get; set;}
        public string UserID { set; get; } 
        public Users Users { set; get; }
    }
}
