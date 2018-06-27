using PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    //TODO: Make it a container that returns concrete implementation of interfaces used for data access
    public class DataProviderFactory
    {
        private static IDataProvider dataProvider;

        public static IDataProvider DataProvider
        {
            get
            {
                if (dataProvider == null)
                {
                    dataProvider = new DbMock();
                }
                return dataProvider;
            }
        }
    }
}
