using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;

namespace Sweeter.Services.DataProviders
{
    public class CategoriesDataProvider : ICategoriesDataProvider
    {
        private IConnectionFactory connection;

        public CategoriesDataProvider(IConnectionFactory factory)
        {
            connection = factory;
        }

        public void AddCategory(CategoriesModel category)
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                sqlConnection.Execute(@"insert into CategoriesTable(Category) values (@Category);",
                new { Category = category.Category});
            }
        }

        public string GetCategoryByID(int ID)
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                string category = sqlConnection.Query<CategoriesModel>("select Category from CategoriesTable where ID = @ID", new { ID = ID }).ToString();
                return category;
            }
        }

        public IEnumerable<CategoriesModel> GetCategories()
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                var categories = sqlConnection.Query<CategoriesModel>("select * from CategoriesTable").AsList();
                return categories;
            }
        }

        public void DeleteCategoryByID(int ID)
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                sqlConnection.Execute(@"delete from CategoriesTable where ID = @ID", new { ID = ID });
            }
        }

        public void DeleteCategoryByName(string Name)
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                sqlConnection.Execute(@"delete from CategoriesTable where Category = @Name", new { ID = Name });
            }
        }
    }
}
