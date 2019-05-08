using Microsoft.AspNetCore.Mvc;

namespace myCoreMvc.Controllers
{
    public class ApiController : BaseController
    {
        [Route("GetClientSocket")]
        public string GetClientSocket()
        {
            var clientIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var clientAddress = clientIP == "::1" ? "localhost" : clientIP;
            var clientPort = Request.HttpContext.Connection.RemotePort;
            return $"{clientAddress}:{clientPort}";
        }
    }
}
