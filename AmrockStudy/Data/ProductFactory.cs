using AmrockStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmrockStudy.Data
{
    public class ProductFactory
    {
        public static IProduct CreateProcut(GeneralProduct input)
        {
            if (input.caption.Contains("glass")){
                if (input.caption.Contains("etching"))
                {
                    return new GlassProduct()
                    {
                        likes = 0,
                        id = input.id,
                        caption = input.caption,
                        Addtions = "None",
                        media_url = input.media_url,
                        Silhouette = "Ford",
                        timestamp = input.timestamp
                    };
                }
                else
                {
                    return new GlassProduct()
                    {
                        likes = 0,
                        id = input.id,
                        caption = input.caption,
                        Addtions =  "vial, moss",
                        media_url = input.media_url,
                        Silhouette = "Dragon",
                        timestamp = input.timestamp
                    };
                }
            }
            else
            {
                return new GeneralProduct()
                {
                    likes = 0,
                    id = input.id,
                    caption = input.caption,
                    media_url = input.media_url,
                    timestamp = input.timestamp
                };
            }
        }
    }
}
