using System;

namespace myCoreMvc.Data
{
    public class SqlClient
    {
        // private readonly IConfiguration config;
        public SqlClient(IConfiguration config) // [FromServices]
        {
            config = config;
        }

        public IConfiguration config { get; }
    }
}
