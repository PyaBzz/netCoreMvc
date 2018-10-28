using Microsoft.AspNetCore.Razor.TagHelpers;

namespace myCoreMvc.PooyasFramework
{
    public class ClientSocketTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var content = @"<div>Your network socket: <span id='client-ip'></span></div>
                            <script>window.onload = function() {
                             var url = window.location.origin + '/GetClientSocket';
                             var ajaxRequest = new XMLHttpRequest();
                             ajaxRequest.onreadystatechange = function() {
                              if (this.readyState === 4 && this.status === 200)
                              {
                               document.getElementById('client-ip').innerHTML = this.responseText;
                              }
                             };
                             ajaxRequest.open('GET', url, true);
                             ajaxRequest.send();
                             };
                            </script>";

            output.Content.SetHtmlContent(content);
        }
    }
}
