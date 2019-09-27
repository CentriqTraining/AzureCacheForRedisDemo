using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace RedisCache4.X.Configuration
{
    public static class RedisConnectionManger
    {
        public static string  GetRedisConnectionString()
        {
            var ConfigLocation = HttpContext.Current.Server.MapPath(@"Configuration/Config.json");
            string ConnectionTemplate = "CentriqRedisDemo.redis.cache.windows.net:6380,password={PWD},ssl=True,abortConnect=False";
            string JsonText = File.ReadAllText(ConfigLocation);
            var result =  JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonText);
            return ConnectionTemplate.Replace("{PWD}",
                    result["secondaryKey"]);
        }
    }
}
