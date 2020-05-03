#pragma checksum "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd1986f6d79a4b45b98beae2d254f2e93945dd6a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Index), @"mvc.1.0.view", @"/Views/Login/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd1986f6d79a4b45b98beae2d254f2e93945dd6a", @"/Views/Login/Index.cshtml")]
    public class Views_Login_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CMS.Models.Helpers.LoginView>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
  
	ViewData["Title"] = "Index";
	Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n<div class=\"logo_con\">\r\n\t<a href=\"https://fasah.sa\"><img src=\"/cms3/new/assets/images/logo_black.png\" /></a>\r\n\t<p>دليل المستخدم للاستيراد والتصدير</p>\r\n</div>\r\n\r\n\r\n\r\n");
#nullable restore
#line 19 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
  
	var errorMessage1 = this.TempData["ErrorMessage"]?.ToString();

	if (!string.IsNullOrEmpty(errorMessage1))
	{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<div class=\"alert alert-danger\">\r\n\t\t\t<strong>");
#nullable restore
#line 25 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
               Write(errorMessage1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n\t\t</div>\r\n");
#nullable restore
#line 27 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("<section id=\"loginform2\">\r\n");
#nullable restore
#line 30 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
     using (Html.BeginForm("Index", "Login", FormMethod.Post, new { @class = "form-vertical form-container", role = "form" }))
	{
		

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
   Write(Html.ValidationSummary(true, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<div class=\"form-group\">\r\n\t\t\t");
#nullable restore
#line 36 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
       Write(Html.LabelFor(model => model.UserName, htmlAttributes: new { @class = "control-label ", @style = "color:#72777c" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t");
#nullable restore
#line 37 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
       Write(Html.EditorFor(model => model.UserName, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t");
#nullable restore
#line 38 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
       Write(Html.ValidationMessageFor(model => model.UserName, "", new { @class = "text-danger", @style = "color:red" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\t\t</div>\r\n\t\t<div class=\"form-group\">\r\n\t\t\t");
#nullable restore
#line 42 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
       Write(Html.LabelFor(model => model.Password, htmlAttributes: new { @class = "control-label", @style = "color:#72777c" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t");
#nullable restore
#line 43 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
       Write(Html.EditorFor(model => model.Password, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t");
#nullable restore
#line 44 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"
       Write(Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger", @style = "color:red" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\t\t</div>\r\n\t\t<button id=\"loginButton\" type=\"submit\" class=\"btn btn-primary btn-block\">LOG IN</button>\r\n");
#nullable restore
#line 48 "C:\CMS\CMS 3.0\CMS\Views\Login\Index.cshtml"

	}

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n\r\n<script type=\"text/javascript\">\r\n$(document).bind(\'keydown\', function (e) {\r\n    if (e.which === 13) { // return\r\n        $(\'#loginButton\').trigger(\'click\');\r\n    }\r\n});\r\n</script>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CMS.Models.Helpers.LoginView> Html { get; private set; }
    }
}
#pragma warning restore 1591