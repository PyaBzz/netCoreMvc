using System.Data.SqlClient;

namespace myCoreMvc.App
{
    public static class SqlConFactory
    {
        public static SqlConnection Get()
        {
            var config = ConfigFactory.Get();
            var connectionStr = config.ConnectionString;
            var connection = new SqlConnection(connectionStr);
            return connection;
        }
    }
}
