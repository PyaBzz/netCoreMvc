using Microsoft.AspNetCore.Razor.TagHelpers;
using PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.PooyasFramework
{
    public class PooyaTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        { //Task: Further develop it to replace complex markup in Razor views or footer markup and script!
            base.Process(context, output);
            output.TagName = "div";
            output.Attributes.SetAttribute("out-att1", "out-val1");
            output.Content.SetContent(context.AllAttributes.Select(a => $"{a.Name} : {a.Value}").ToString(" | "));
        }
    }
}
