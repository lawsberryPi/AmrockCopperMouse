using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmrockStudy.Models
{
    public interface IProduct
    {

        string id { get; set; }
        string media_url { get; set; }
    }
    public class GeneralProduct:IProduct
    {
        [Key]
        public string id { get; set; }
        public int likes { get; set; }
        public string caption { get; set; }
        public DateTime timestamp { get; set; }
        public string media_url { get; set; }
        public string thumbnail_url { get; set; }
        public ICollection<Orders> Orders { get; set; } 
    }


    public class GlassProduct: GeneralProduct
    {
        public string Silhouette { set; get; }
        public string Addtions { set; get; }
    }

    public class DigitalProduct: GeneralProduct
    {
        public string DownloadMethod { set; get; }
    }
}
