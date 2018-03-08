using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace myCoreMvc.Controllers
{
    public class ExperimentsController : Controller
    {
        //[Route("delegate")]
        public ContentResult Delegate()
        {
            var result = "";

            DelegateType hasang;

            hasang = Delegates.DelegateImplementation1;
            result += hasang(8) + Environment.NewLine;

            hasang -= Delegates.DelegateImplementation1;
            hasang += Delegates.DelegateImplementation2;
            result += hasang(8) + Environment.NewLine;

            return Content(result);
        }

        //[Route("extension")]
        public ContentResult Extension()
        {
            return Content($"8 factorial is {8.Factorial()}" + Environment.NewLine);
        }
    }
}
