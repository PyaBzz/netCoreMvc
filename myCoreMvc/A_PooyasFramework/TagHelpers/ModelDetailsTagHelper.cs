using Microsoft.AspNetCore.Razor.TagHelpers;
using PooyasFramework;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.PooyasFramework
{
    [HtmlTargetElement("ModelDetails")]
    public class ModelDetailsTagHelper : TagHelper
    {
        public Thing TagModel { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            var existingContent = await output.GetChildContentAsync();
            var requestPath = $"/DetailsOf{TagModel.GetType().Name}/Delete/{TagModel.Id}";
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
                               $"var url = window.location.origin + '{requestPath}';" +
                               "window.location.href = url;" +
                              "}" +
                             "};" +
                            "</script>";
            output.Content.AppendHtml(existingContent);
            output.Content.AppendHtml(content);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
