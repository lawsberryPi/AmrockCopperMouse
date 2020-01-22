using AmrockStudy.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AmrockStudy.Data
{
    public static class InstaInit
    {
        public static void Initialize(ECommerceContext context)
        {
            context.Database.EnsureCreated();

            if (context.GeneralProduct.Any())
            {
                return;   // DB has been seeded
            }

            var productList = getImageUrl();
            foreach(var eachProduct in productList)
            {
                //factory pattern, you give it a parameter, it will return you a object
                var productObject = ProductFactory.CreateProcut(eachProduct);
                Console.WriteLine(productObject);
                if (productObject.GetType() == typeof(GlassProduct))
                {
                    GlassProduct additionProduct = (GlassProduct) productObject;
                    context.GlassProduct.Add(additionProduct);
                }else if(productObject.GetType() == typeof(GeneralProduct))
                {
                    GeneralProduct additionProduct = (GeneralProduct)productObject;
                    context.GeneralProduct.Add(additionProduct);
                }
            }
            context.SaveChanges();

        }
        private static async Task<string> GetImagesList(string id)
        {
            string requestUrl = "https://graph.instagram.com/" + id + "?fields=media_url,caption,thumbnail_url,timestamp&access_token=" + 
                "IGQVJXb2lKdjQ3RjJONXgzdk1HR1pTSV83aUtoXzJ2Ni0ySnBBSnc3dGc0OHBsTzAyeDJnVWtSUHBvVTVvOEJXWEwxR0QwZA0tGWWlfRTJGUjF3aEJ2REc5bUZA5OXhQbmF0M0tZAMzVR";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        public static List<GeneralProduct> getImageUrl()
        {
            var allImages = new List<GeneralProduct>();
            var result = InstaData.retriveList();
            List<Task<string>>requestUrlTasks = new List<Task<string>>();
            foreach(var eachUrl in result)
            {
                var task = Task.Run(async () => await GetImagesList(eachUrl));
                requestUrlTasks.Add(task);
            }
            Task.WaitAll(requestUrlTasks.ToArray());
            foreach(var eachTask in requestUrlTasks)
            {
                var reachResult = eachTask.Result;
                GeneralProduct items = JsonConvert.DeserializeObject<GeneralProduct>(reachResult);
                allImages.Add(items);
            }
            return allImages;
        }
    }
}
