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
    public class ApiController : Controller
    {
        [Route("GetClientSocket")]
        public string GetClientSocket()
        {
            var clientIP = Request.HttpContext.Connection.RemoteIpAddress;
            var clientPort = Request.HttpContext.Connection.RemotePort;
            return $"{clientIP}:{clientPort}";
        }
    }
}
