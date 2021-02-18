using System;
using Xunit;

namespace DataTest
{
    public class SqlClientTest
    {
        [Fact]
        public void Constructor_GetsAConfigObject(SqlClient client)
        {
            Assert.NotNull(client.config);
        }
    }
}
