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
            var result = new { FirstName = "Chen", LastName = "Angelo" };
            Console.WriteLine("In Action Main");
            return Ok(result);
        }
    }
}