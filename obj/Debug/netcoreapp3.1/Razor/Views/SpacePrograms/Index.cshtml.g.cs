#pragma checksum "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5ab0177f631ab93dc9355cd43020d7dc60408cad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SpacePrograms_Index), @"mvc.1.0.view", @"/Views/SpacePrograms/Index.cshtml")]
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
#line 1 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\_ViewImports.cshtml"
using SpaceAgenciesDatabaseApp;

#line default
#line hidden
#line 2 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\_ViewImports.cshtml"
using SpaceAgenciesDatabaseApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ab0177f631ab93dc9355cd43020d7dc60408cad", @"/Views/SpacePrograms/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f0135c7ea5dada8bba117669562e0327e6debab0", @"/Views/_ViewImports.cshtml")]
    public class Views_SpacePrograms_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SpaceAgenciesDatabaseApp.SpacePrograms>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#line 3 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
  
    ViewData["Title"] = "Space programs";

#line default
#line hidden
            WriteLiteral("\r\n<h1>");
#line 7 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
Write(ViewBag.AgencyName);

#line default
#line hidden
            WriteLiteral(" program </h1>\r\n\r\n<p>\r\n    ");
#line 10 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
Write(Html.ActionLink("Create new program", "Create", new {agencyId = @ViewBag.AgencyId} ));

#line default
#line hidden
            WriteLiteral("\r\n\r\n</p>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#line 18 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Title));

#line default
#line hidden
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#line 21 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.StartDate));

#line default
#line hidden
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#line 24 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.EndDate));

#line default
#line hidden
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#line 27 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Target));

#line default
#line hidden
            WriteLiteral("\r\n            </th>\r\n");
            WriteLiteral("        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#line 35 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#line 38 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Title));

#line default
#line hidden
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#line 41 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.StartDate));

#line default
#line hidden
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#line 44 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.EndDate));

#line default
#line hidden
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#line 47 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Target));

#line default
#line hidden
            WriteLiteral("\r\n            </td>\r\n");
            WriteLiteral("            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ab0177f631ab93dc9355cd43020d7dc60408cad7500", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 64 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
                                       WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ab0177f631ab93dc9355cd43020d7dc60408cad9640", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 65 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
                                          WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5ab0177f631ab93dc9355cd43020d7dc60408cad11786", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 66 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
                                         WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#line 69 "D:\CSharp\SpaceAgenciesDatabaseApp\SpaceAgenciesDatabaseApp\Views\SpacePrograms\Index.cshtml"
}

#line default
#line hidden
            WriteLiteral("    </tbody>\r\n</table>\r\n<div class =\"container\">\r\n    <div id=\"chart2\"></div>\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@" 
    <script type=""text/javascript"" src=""https://www.gstatic.com/charts/loader.js""></script>
    <script>
        google.charts.load('current', { packages: ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart(){
            $.get('/api/Charts/ProgramsJsonData', function (jsonData) {
                data = google.visualization.arrayToDataTable(jsonData, false);
                    var option = {
                        title: ""Missions to programs ratio"",
                        width: 800,
                        height: 600,
                        
                    };
                chart = new google.visualization.ColumnChart(document.getElementById('chart2'));
                chart.draw(data, option);
            })
        }
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SpaceAgenciesDatabaseApp.SpacePrograms>> Html { get; private set; }
    }
}
#pragma warning restore 1591
