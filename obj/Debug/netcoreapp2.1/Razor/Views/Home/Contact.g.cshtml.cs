#pragma checksum "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/Home/Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dce5c8ef54caea8164be0fc7d5ead3108991c47c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Contact), @"mvc.1.0.view", @"/Views/Home/Contact.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Contact.cshtml", typeof(AspNetCore.Views_Home_Contact))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/_ViewImports.cshtml"
using aspcore_testRedis;

#line default
#line hidden
#line 2 "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/_ViewImports.cshtml"
using aspcore_testRedis.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dce5c8ef54caea8164be0fc7d5ead3108991c47c", @"/Views/Home/Contact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76b164f0dfce844bd54cf976a0fdcd38864fa2ba", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/Home/Contact.cshtml"
  
    ViewData["Title"] = "Contact";

#line default
#line hidden
            BeginContext(43, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(48, 17, false);
#line 4 "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/Home/Contact.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(65, 11, true);
            WriteLiteral("</h2>\r\n<h3>");
            EndContext();
            BeginContext(77, 19, false);
#line 5 "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/Home/Contact.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(96, 151, true);
            WriteLiteral("</h3>\r\n\r\n<address>\r\n    One Microsoft Way<br />\r\n    Redmond, WA 98052-6399<br />\r\n    <abbr title=\"Phone\">P:</abbr>\r\n    425.555.0100\r\n</address>\r\n<b>");
            EndContext();
            BeginContext(248, 13, false);
#line 13 "/Users/chen/MyWork/Dotnet/aspcore-testRedis/Views/Home/Contact.cshtml"
Write(ViewBag.Hello);

#line default
#line hidden
            EndContext();
            BeginContext(261, 227, true);
            WriteLiteral("</b>\r\n\r\n<address>\r\n    <strong>Support:</strong> <a href=\"mailto:Support@example.com\">Support@example.com</a><br />\r\n    <strong>Marketing:</strong> <a href=\"mailto:Marketing@example.com\">Marketing@example.com</a>\r\n</address>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
