using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.Services.DataProviders
{
    public interface ICategoriesDataProvider
    {
        void AddCategory(CategoriesModel category);
        CategoriesModel GetCategoryByID(int ID);
        IEnumerable<CategoriesModel> GetCategories();
        void DeleteCategoryByID(int ID);
        void DeleteCategoryByName(string Name);
        CategoriesModel GetCategoryByName(string categoryname);

    }
}
