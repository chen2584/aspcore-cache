using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class OutputCacheAttribute : ActionFilterAttribute
{
    private static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
    private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
    public int Duration { get; set; } = 30;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string cacheKey = $"{context.RouteData.Values["controller"].ToString()}-{context.RouteData.Values["action"].ToString()}";
        if (cache.TryGetValue(cacheKey, out OutputCacheModel outputCache))
        {
            context.Result = new ContentResult
            {
                ContentType = outputCache.ContentType,
                StatusCode = outputCache.StatusCode,
                Content = outputCache.Content
            };
            Console.WriteLine("Return Cache");
        }

    }

    public override async void OnResultExecuted(ResultExecutedContext context)
    {
        string cacheKey = $"{context.RouteData.Values["controller"].ToString()}-{context.RouteData.Values["action"].ToString()}";
        Console.WriteLine("Duration is " + Duration);
        if (cache.Get(cacheKey) == null)
        {
            Console.WriteLine("Cache null");
            if (context.Result is ObjectResult objectResult)
            {
                string value = context.HttpContext.Response.ContentType.Contains("application/json") ? JsonConvert.SerializeObject(objectResult.Value, jsonSettings) : objectResult.Value.ToString();
                var outputCache = new OutputCacheModel() { Content = value, ContentType = context.HttpContext.Response.ContentType, StatusCode = context.HttpContext.Response.StatusCode };
                cache.Set(cacheKey, outputCache, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Duration)));
            }
            else if (context.Result is ViewResult viewResult)
            {
                Console.WriteLine($"\n\nActionResult is: {viewResult}\n\n");

                string viewName = viewResult.ViewName != null ? viewResult.ViewName : context.RouteData.Values["action"].ToString();
                using (var writer = new StringWriter())
                {
                    IViewEngine viewEngine = context.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                    ViewEngineResult viewEngineResult = viewEngine.FindView(context, viewName, true);

                    if (viewEngineResult.Success)
                    {
                        ViewContext viewContext = new ViewContext(context, viewEngineResult.View, viewResult.ViewData, viewResult.TempData, writer, new HtmlHelperOptions());
                        await viewEngineResult.View.RenderAsync(viewContext);

                        string content = writer.GetStringBuilder().ToString();
                        var outputCache = new OutputCacheModel() { Content = content, ContentType = context.HttpContext.Response.ContentType, StatusCode = context.HttpContext.Response.StatusCode };
                        cache.Set(cacheKey, outputCache, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Duration)));

                        Console.WriteLine("Result is : ");
                    }
                    else
                    {
                        Console.WriteLine($"A view with the name {viewName} could not be found");
                    }
                }
            }
        }
    }
}