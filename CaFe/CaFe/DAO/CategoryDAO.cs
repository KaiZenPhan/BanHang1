using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return CategoryDAO.instance;
            }
            private set
            {
                CategoryDAO.instance = value;
            }
        }
        private CategoryDAO()
        {

        }

        public List<Category> getListCategory()
        {
            List<Category> listCategory = new List<Category>();
            string query = "EXECUTE dbo.USP_getListCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            return listCategory;
        }
        public bool AddCategory(string name)
        {
            string query = "EXECUTE dbo.USP_addCategory @namecategory";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name });
            return result > 0;
        }
        public bool UpdateCategory(int id , string name)
        {
            string query = "EXECUTE dbo.USP_updateCategory @id , @namecategory";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id,name });
            return result > 0;
        }
        public bool DeleteCategory(int id)
        {
            // xóa các thức ăn có chứa id của category này

            FoodDAO.Instance.DeleteFoodByIdCategory(id);
            string query = "EXECUTE dbo.USP_deleteCategory @idcategory";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id});
            return result > 0;
        }
        public List<Category> SearchCategory(string name)
        {
            List<Category> listCategory = new List<Category>();
            string query = "EXECUTE dbo.USP_getlistCategoryBySearching @name";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { name});
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            return listCategory;
        }
    }
    
    
}
