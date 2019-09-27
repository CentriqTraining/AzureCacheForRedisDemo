using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheCore.configuration
{
    public static class RedisConnectionManger
    {
        public static string  GetRedisConnectionString()
        {
            string ConnectionTemplate = "CentriqRedisDemo.redis.cache.windows.net:6380,password={PWD},ssl=True,abortConnect=False";
            string JsonText = File.ReadAllText(@"Configuration\Config.json");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonText);
            return ConnectionTemplate.Replace("{PWD}",
                    result["secondaryKey"]);
        }
    }
}
