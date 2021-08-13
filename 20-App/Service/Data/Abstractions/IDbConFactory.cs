using System.Data.SqlClient;

namespace myCoreMvc.App.Services
{
    public interface IDbConFactory
    {
        SqlConnection Get();
        SqlConnection GetInit();
    }
}
