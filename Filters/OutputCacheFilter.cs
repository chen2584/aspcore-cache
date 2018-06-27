using System;
using System.IO;
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

public class OutputCacheAttribute : ActionFilterAttribute
{
    private static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string controllerName = context.RouteData.Values["controller"].ToString();
        string actionName = context.RouteData.Values["action"].ToString();
        OutputCacheModel outputCache;
        if (cache.TryGetValue($"{controllerName}-{actionName}", out outputCache))
        {
            Console.WriteLine($"{outputCache.Content}, {outputCache.ContentType}");
            context.Result = new ContentResult
            {
                ContentType = outputCache.ContentType,
                StatusCode = (int)HttpStatusCode.OK,
                Content = outputCache.Content
            };
            Console.WriteLine("Return Cache");
        }
    }

    public override async void OnResultExecuted(ResultExecutedContext context)
    {
        string controllerName = context.RouteData.Values["controller"].ToString();
        string actionName = context.RouteData.Values["action"].ToString();
        if (context.Result is ObjectResult objectResult)
        {
            if (objectResult.Value != null)
            {  
                var outputCache = new OutputCacheModel() { Content = objectResult.Value.ToString(), ContentType = "application/json; charset=utf-8" };
                cache.Set($"{controllerName}-{actionName}", outputCache, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));
            }
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

                    var outputCache = new OutputCacheModel() { Content = content, ContentType = "text/html" };
                    cache.Set($"{controllerName}-{actionName}", outputCache, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)));

                    Console.WriteLine("Result is : ");
                    Console.WriteLine(writer.GetStringBuilder().ToString());
                }
                else
                {
                    Console.WriteLine($"A view with the name {viewName} could not be found");
                }
            }
        }
    }
}
public static class ControllerExtensions
{
    public static async Task<string> RenderViewAsync(this Controller controller, string viewName, bool partial = false)
    {
        if (string.IsNullOrEmpty(viewName))
        {
            viewName = controller.ControllerContext.ActionDescriptor.ActionName;
        }

        //controller.ViewData.Model = model;

        using (var writer = new StringWriter())
        {
            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

            if (viewResult.Success == false)
            {
                return $"A view with the name {viewName} could not be found";
            }

            ViewContext viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            return writer.GetStringBuilder().ToString();
        }
    }
}