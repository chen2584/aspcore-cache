using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_testRedis.Controllers
{
    public class ChenController : Controller
    {
        [HttpGet]
        [OutputCache]
        public ActionResult Index()
        {
            //var returnz = await this.RenderViewAsync("About", false);
            var result = new object[] { new { FirstName = "Chen", LastName = "Angelo" }, new { FirstName = "Chen2", LastName = "Angelo2" } };
            Console.WriteLine("In Action Main");
            return Ok(result);
        }
    }
}