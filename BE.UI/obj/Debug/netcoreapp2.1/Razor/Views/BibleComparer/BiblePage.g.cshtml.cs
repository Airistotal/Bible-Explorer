#pragma checksum "C:\Users\Daniel\Documents\Repos\Bible Explorer\BE.UI\Views\BibleComparer\BiblePage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "441c3fd405fb4b614e61970e8217d2c8898428af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BibleComparer_BiblePage), @"mvc.1.0.view", @"/Views/BibleComparer/BiblePage.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BibleComparer/BiblePage.cshtml", typeof(AspNetCore.Views_BibleComparer_BiblePage))]
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
#line 1 "C:\Users\Daniel\Documents\Repos\Bible Explorer\BE.UI\Views\_ViewImports.cshtml"
using BE.UI;

#line default
#line hidden
#line 2 "C:\Users\Daniel\Documents\Repos\Bible Explorer\BE.UI\Views\_ViewImports.cshtml"
using BE.UI.Models;

#line default
#line hidden
#line 1 "C:\Users\Daniel\Documents\Repos\Bible Explorer\BE.UI\Views\BibleComparer\BiblePage.cshtml"
using BE.Comparer.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"441c3fd405fb4b614e61970e8217d2c8898428af", @"/Views/BibleComparer/BiblePage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e7981516d9c769dc80420780d91c504b3beca666", @"/Views/_ViewImports.cshtml")]
    public class Views_BibleComparer_BiblePage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BibleChapter>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(27, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(50, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(53, 32, false);
#line 5 "C:\Users\Daniel\Documents\Repos\Bible Explorer\BE.UI\Views\BibleComparer\BiblePage.cshtml"
Write(Html.Partial("PageHeader", null));

#line default
#line hidden
            EndContext();
            BeginContext(85, 5, true);
            WriteLiteral(";\r\n\r\n");
            EndContext();
            BeginContext(91, 37, false);
#line 7 "C:\Users\Daniel\Documents\Repos\Bible Explorer\BE.UI\Views\BibleComparer\BiblePage.cshtml"
Write(Html.Action("ComparedContent", Model));

#line default
#line hidden
            EndContext();
            BeginContext(128, 3, true);
            WriteLiteral(";\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BibleChapter> Html { get; private set; }
    }
}
#pragma warning restore 1591
