using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using RedisCache4.X.Configuration;
using RedisCache4.X.Models;
using StackExchange.Redis;

namespace RedisCache4.X.Controllers
{
    public class HomeController : Controller
    {
        private List<Employee> _Emps = null;
        public HomeController()
        {
            var RedisManager = ConnectionMultiplexer.Connect(RedisConnectionManger.GetRedisConnectionString());
            IDatabase Redis = RedisManager.GetDatabase();

            string CachedValue = Redis.StringGet("Emps");

            if (CachedValue == null)
            {
                _Emps = GetData();
                Redis.StringSet("Emps", JsonConvert.SerializeObject(_Emps), TimeSpan.FromMinutes(4));
            }
            else
            {
                _Emps = JsonConvert.DeserializeObject<List<Employee>>(CachedValue);
            }
        }
        public ActionResult Index()
        {
            return View(_Emps);
        }
        private List<Employee> GetData()
        {
            return new List<Employee>()
            {
                new Employee() { FirstName = "Scooby", LastName = "Doo", ID = 1 },
                new Employee() { FirstName = "Scrappy", LastName = "Doo", ID=2 },
                new Employee() { FirstName = "Shaggy", LastName = "Rogers", ID=3 },
                new Employee() { FirstName = "Fred", LastName = "Jones", ID=4 },
                new Employee() { FirstName = "Daphne", LastName = "Blake",ID=5 },
                new Employee() { FirstName = "Velma", LastName = "Dinkley",ID=6 },
            };
        }
    }
}
