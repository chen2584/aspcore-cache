using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspcore_testRedis.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace aspcore_testRedis.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IDistributedCache distributedCache;
        private readonly IMemoryCache cache;
        public HomeController(IMemoryCache cache)
        {
            //this.distributedCache = distributedCache;
            this.cache = cache;
        }

        [ResponseCache(Duration = 30)]
        public JsonResult GetAllCustomers()
        {
            List<object> customers;
            if (!cache.TryGetValue("CustomersList", out customers))
            {
                Console.WriteLine("Customers Value: " + customers == null ? "Null" : "Mai null");
                if (customers == null)
                {
                    Console.WriteLine("Set Cache");
                    customers = AllCustomer();
                }
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(20));

                cache.Set("CustomersList", customers, cacheEntryOptions);

            }
            Console.WriteLine(Request.Path.Value);
            Console.WriteLine($"Controller: {RouteData.Values["controller"]} Action: {RouteData.Values["action"]} ");
            return Json(customers);

        }

        private List<object> AllCustomer()
        {
            var customers = new List<object>();
            customers.Add(new { Id = 1, Name = "Kalpesh" });
            customers.Add(new { Id = 2, Name = "Ajay" });
            return customers;
        }

        [OutputCache]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            /*distributedCache.SetString("ChenNum", "1");
            distributedCache.SetString("ChenName", "Worameth Semapat");
            Console.WriteLine("GetString: " + distributedCache.GetString("ChenName"));
            ViewData["Message"] = distributedCache.GetString("ChenName");*/
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [OutputCache(Duration=5)]
        public ActionResult Chen()
        {
            ViewData["Message"] = "This is a book";
            ViewBag.Hello = "Hello View Bag";
            return View("contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
