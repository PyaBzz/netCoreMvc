using System.Data.SqlClient;

namespace myCoreMvc.App
{
    public static class SqlConFactory
    {
        public static SqlConnection Get()
        {
            var config = ConfigFactory.Get();
            var connectionStr = config.ConnectionStr["Normal"];
            var connection = new SqlConnection(connectionStr);
            return connection;
        }

        public static SqlConnection GetInit() //Todo: Find a good pattern for these two similar methods
        {
            var config = ConfigFactory.Get();
            var connectionStr = config.ConnectionStr["Init"];
            var connection = new SqlConnection(connectionStr);
            return connection;
        }
    }
}
