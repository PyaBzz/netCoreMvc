using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myCoreMvc.Models;
using System.Data.SqlClient;

namespace myCoreMvc.Services
{
    public class Db
    {
        public static T Get<T>(int id)
        {
            var myDBconnection = new SqlConnection("Database=myCoreMvc; SERVER=.\\sqlexpress; Integrated Security=SSPI; MultipleActiveResultSets=True;");
            myDBconnection.Open();
            var myParam = new SqlParameter("@Param1", id);
            var selectCommand = new SqlCommand("select * from [myCoreMvc].[dbo].[WorkItems] where Id = @Param1", myDBconnection);
            selectCommand.Parameters.Add(myParam);
            var myReader = selectCommand.ExecuteReader();
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var instance = (T)Activator.CreateInstance(typeof(T));
            if (myReader.Read())
            {
                foreach (var property in properties)
                {
                    typeof(T).GetProperty(property.Name).SetValue(instance, myReader[property.Name]);
                }
            }
            myDBconnection.Close();
            return instance;
        }

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
}
