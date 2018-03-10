using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace myCoreMvc.Controllers
{
    public class ExperimentsController : Controller
    {
        [Route("Db")]
        public ContentResult Db()
        {
            var result = string.Empty;
            var myDBconnection = new SqlConnection("Database=myCoreMvc; SERVER=.\\sqlexpress; Integrated Security=SSPI; MultipleActiveResultSets=True;");
            myDBconnection.Open();
            var myParam = new SqlParameter("@Param1", System.Data.SqlDbType.Int);
            myParam.Value = "5";
            var selectCommand = new SqlCommand("select * from [myCoreMvc].[dbo].[WorkItems] where Id = @Param1", myDBconnection);
            selectCommand.Parameters.Add(myParam);
            var myReader = selectCommand.ExecuteReader();
            if (myReader.Read())
            {
                for (var i = 0; i < myReader.FieldCount; i++)
                {
                    result += " " + myReader[i].ToString();
                }
            }
            myDBconnection.Close();
            return Content(result);
            //    [Route("Add")]
            //public string Add()
            //{
            //    var myDBconnection = new SqlConnection("Database=Prime247.Temp; SERVER=.\\sqlexpress; Integrated Security=SSPI; MultipleActiveResultSets=True;");
            //    var insertCommand = new SqlCommand("insert into [Prime247.Temp].[dbo].[Banks] (Id, Name, AccountNumber, SortCode, AccountName) values ('d83b1b6e-5e14-476e-9f05-cff01d35a7e9', 'DasooName', '321', '123', 'DasooAccountName')", myDBconnection);
            //    myDBconnection.Open();
            //    insertCommand.ExecuteNonQuery();
            //    myDBconnection.Close();
            //    return "Added";
            //}

        }

        [Route("delegate")]
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

        [Route("extension")]
        public ContentResult Extension()
        {
            return Content($"8 factorial is {8.Factorial()}" + Environment.NewLine);
        }
    }
}
