using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;
using myCoreMvc.Services;
using myCoreMvc.Models;
using PooyasFramework;

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
