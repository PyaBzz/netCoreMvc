using Microsoft.AspNetCore.Razor.TagHelpers;
using PooyasFramework;
using System.Linq;

namespace myCoreMvc.PooyasFramework
{ //Task: Move Delete button and associated JS from view files to here.
    [HtmlTargetElement("ModelDetails", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModelDetailsTagHelper : TagHelper
    {
        public object TagModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            var content = $@"<table style='width: 100%'>" +
                             "<tbody>" +
                              TagModel.GetType().GetPublicInstancePropertyInfos().Select(pi =>
                               $"<tr>" +
                                $"<td style='width: 30%; border: 2px solid cornflowerblue'>{pi.Name}</td>" +
                                $"<td style='width: 70%; border: 2px solid deepskyblue'>{pi.GetValue(TagModel)}</td>" +
                               $"</tr>").ToString("") +
                             "</tbody>" +
                            "</table>";
            output.Content.SetHtmlContent(content);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
