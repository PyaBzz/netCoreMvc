using System.Data.SqlClient;

namespace myCoreMvc.App
{
    public static class SqlConFactory
    {
        public static SqlConnection Get(bool isInit = false)
        {
            var config = ConfigFactory.Get();
            var connectionStr = isInit
            ? config.Database.ConnectionStr["Init"]
            : config.Database.ConnectionStr["Normal"];
            return new SqlConnection(connectionStr);
        }
    }
}
