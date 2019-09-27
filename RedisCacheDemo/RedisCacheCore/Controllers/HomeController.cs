using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisCacheCore.Models;

namespace RedisCacheCore.Controllers
{
    public class HomeController : Controller
    {
        private List<Employee> _Emps = null;
        public HomeController(IDistributedCache redis)
        {
            string CachedValue = redis.GetString("emps");

            if (CachedValue == null)
            {
                _Emps = GetData();
                DistributedCacheEntryOptions itemOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(4)
                };
                redis.SetString("emps", JsonConvert.SerializeObject(_Emps), itemOptions);
            }
            else
            {
                _Emps = JsonConvert.DeserializeObject<List<Employee>>(CachedValue);
            }
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

        public IActionResult Index()
        {
            return View(_Emps);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
