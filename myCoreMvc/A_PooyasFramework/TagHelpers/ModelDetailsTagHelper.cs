using Microsoft.AspNetCore.Razor.TagHelpers;
using PooyasFramework;
using System.Linq;

namespace myCoreMvc.PooyasFramework
{ //Task: Find a way to move the Edit button to here as well.
    [HtmlTargetElement("ModelDetails", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModelDetailsTagHelper : TagHelper
    {
        public Thing TagModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            var request = $"/DetailsOf{TagModel.GetType().Name}/Delete/{TagModel.Id}";
            var content = $"<table style='width: 100%'>" +
                            "<button id='delete'>Delete</button>" +
                            "<tbody>" +
                             TagModel.GetType().GetPublicInstancePropertyInfos().Select(pi =>
                              $"<tr>" +
                               $"<td style='width: 30%; border: 2px solid cornflowerblue'>{pi.Name}</td>" +
                               $"<td style='width: 70%; border: 2px solid deepskyblue'>{pi.GetValue(TagModel)}</td>" +
                              $"</tr>").ToString("") +
                            "</tbody>" +
                            "</table>" +

                            "<script>" +
                             "var deleteButton = document.getElementById('delete');" +
                             "deleteButton.onclick = function() {" +
                              $"var confirmed = confirm('Are you sure you want to delete this {TagModel.GetType().Name}?');" +
                              "if (confirmed) {" +
                               $"var url = window.location.origin + '{request}';" +
                               "window.location.href = url;" +
                              "}" +
                             "};" +
                            "</script>";

            output.Content.AppendHtml(content);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
