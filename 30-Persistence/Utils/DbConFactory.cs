using System.Data.SqlClient;
using myCoreMvc.App;

namespace myCoreMvc.Persistence
{
    public class DbConFactory : IDbConFactory
    {
        public SqlConnection Get()
        {
            var config = ConfigFactory.Get();
            var connectionStr = config.Data.ConnectionStr["Prod"];
            return new SqlConnection(connectionStr);
        }

        public SqlConnection GetInit()
        {
            var config = ConfigFactory.Get();
            var connectionStr = config.Data.ConnectionStr["Init"];
            return new SqlConnection(connectionStr);
        }
    }
}
