using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmrockStudy.Data
{
    public static class InstaData
    {
        public static List<string> retriveList()
        {
            var returnString = new List<string>() { };
            using (StreamReader r = new StreamReader("insta.json"))
            {
                string json = r.ReadToEnd();
                List<InstaListData> items = JsonConvert.DeserializeObject<List<InstaListData>>(json);
                returnString = items.Select(o => o.id).ToList();
            }
            return returnString;
        }
    }
    public class InstaListData
    {
        public string id { set; get; }
    }
}
