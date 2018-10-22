using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myCoreMvc.Models;
using System.Data.SqlClient;
using PooyasFramework;

namespace myCoreMvc.Services
{
    public class Db : IDataProvider
    {
        //Task: Move this to config files.
        static string connectionString = "SERVER=.\\sqlexpress; Database=myCoreMvc; MultipleActiveResultSets=True; Integrated Security=SSPI;";
        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        private static string GetTableName(Type T)
        {
            return "WorkItems"; //Task: Must be based on type of T
        }

        private static T Construct<T>(SqlDataReader sqlReader)
        {
            var propertyInfos = typeof(T).GetPublicInstancePropertyInfos();
            var columns = sqlReader.GetNonRelationalColumnNames();
            var commonProperties = propertyInfos.Where(pi => columns.Contains(pi.Name));
            var instance = (T)Activator.CreateInstance(typeof(T));
            if (sqlReader.Read())
            {
                foreach (var propertyInfo in commonProperties)
                {
                    typeof(T).GetProperty(propertyInfo.Name).SetValue(instance, sqlReader[propertyInfo.Name]);
                }
            }
            return instance;
        }

        //Task: Remove this
        public static T Get<T>(Guid id)
        {
            var itemType = typeof(T);
            var itemId = new SqlParameter("@itemId", id);
            var tableName = new SqlParameter("@tableName", GetTableName(itemType));
            var selectCommand = new SqlCommand($@"exec('select * from ' + @tableName + ' where Id = ''' + @itemId + '''');", sqlConnection);
            selectCommand.Parameters.AddRange(new[] { tableName, itemId });
            sqlConnection.Open();
            var myReader = selectCommand.ExecuteReader();
            var instance = Construct<T>(myReader);
            sqlConnection.Close();
            return instance;
        }

        public TransactionResult Add<T>(T obj) where T : Thing
        {
            var insertCommand =
                new SqlCommand(@"insert into [WorkItems]
                    (Id, Reference, Priority, Name, WorkPlan)
                    values ('d83b1b6e-5e14-476e-9f05-cff01d35a7e9', 'DI1', '3', 'DasooItem', '53c88402-4092-4834-8e7f-6ce70057cdc5')", sqlConnection);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return TransactionResult.Added;
        }

        //Task: Needs serious refactoring
        public static List<T> GetList<T>()
        {
            var targetType = typeof(T);
            //Lesson:
            #region
            // We use SQL parameters to prevent SQL Injection.
            // Consider what would happen if TextBox1.Text made its way through a string placeholder to the resulting SqlCommand.
            // It could contain 1';DROP TABLE myTable;'.
            // SqlParameters won't allow suspicious patterns.
            #endregion
            var tableName = new SqlParameter("@tableName", GetTableName(targetType));
            var getListCommand = new SqlCommand(@"exec('select * from ' + @tableName);", sqlConnection);
            getListCommand.Parameters.Add(tableName);
            sqlConnection.Open();
            var reader = getListCommand.ExecuteReader();
            var columns = reader.GetNonRelationalColumnNames();
            var propertyInfos = targetType.GetPublicInstancePropertyInfos();
            var commonProperties = propertyInfos.Where(pi => columns.Contains(pi.Name));
            var targetListType = typeof(List<>).MakeGenericType(targetType);
            var result = (List<T>)Activator.CreateInstance(targetListType);

            while (reader.Read())
            {
                var instance = (T)Activator.CreateInstance(targetType);
                foreach (var propertyInfo in commonProperties)
                {
                    typeof(T).GetProperty(propertyInfo.Name).SetValue(instance, reader[propertyInfo.Name]);
                }
                result.Add(instance);
            }
            sqlConnection.Close();
            return result;
        }

        public TransactionResult Delete<T>(Guid id) where T : Thing
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList<T>(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public TransactionResult Update<T>(T obj) where T : Thing
        {
            throw new NotImplementedException();
        }

        //Task: Get rid of these
        public static TransactionResult Add()
        {
            var insertCommand =
                new SqlCommand(@"insert into [myCoreMvc].[dbo].[WorkItems]
                    (Id, Reference, Priority, Name, WorkPlan)
                    values ('d83b1b6e-5e14-476e-9f05-cff01d35a7e9', 'DI1', '3', 'DasooItem', '53c88402-4092-4834-8e7f-6ce70057cdc5')", sqlConnection);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return TransactionResult.Added;
        }

        T IDataProvider.Get<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        List<T> IDataProvider.GetList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
