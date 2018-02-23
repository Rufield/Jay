using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;
using System.Linq;

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

        public CategoriesModel GetCategoryByID(int ID)
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                var category = sqlConnection.Query<CategoriesModel>("select * from CategoriesTable where ID = @id", new { id = ID }).First();
                return category;
            }
        }
        public CategoriesModel GetCategoryByName(string categoryname)
        {
            using (var sqlConnection = connection.CreateConnection)
            {
                var category = sqlConnection.Query<CategoriesModel>("select * from CategoriesTable where Category = @Category", new { Category = categoryname }).First();
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
