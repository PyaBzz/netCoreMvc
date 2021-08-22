using System.Data.SqlClient;

namespace myCoreMvc.Persistence
{
    public interface IDbConFactory
    {
        SqlConnection Get();
        SqlConnection GetInit();
    }
}
