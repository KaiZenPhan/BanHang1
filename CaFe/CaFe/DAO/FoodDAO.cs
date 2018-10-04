using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodDAO();
                }
                return FoodDAO.instance;
            }
            private set
            {
                FoodDAO.instance = value;
            }
        }
        private FoodDAO()
        {

        }
        public List<Food> getListFoodByCategoryId(int idCategory)
        {
            List<Food> listFood = new List<Food>();
            string query = "EXECUTE dbo.USP_getListFoodByCategoryId @idCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idCategory });
            foreach(DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }

        public List<ListFood> getListFood()
        {
            List<ListFood> Listfood = new List<ListFood>();
            string query = "EXECUTE dbo.USP_getListFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                ListFood food = new ListFood(item);
                Listfood.Add(food);
            }
            return Listfood;
        }
        public bool insertFood(string name , int idcategory , double price)
        {
            string query = "EXECUTE dbo.USP_addFood @name , @idcategory , @price";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, idcategory, price });
            return result > 0;
        }
        public bool updateFood(int id , string name, int idcategory, double price)
        {
            string query = "EXECUTE dbo.USP_updateFood @id , @name , @idcategory , @price";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id,name, idcategory, price });
            return result > 0;
        }
        public bool deleteFood(int idfood)
        {
            BillInFoDAO.Instance.DeleteBillInFoByIdFood(idfood);
            string query = "EXECUTE dbo.USP_deleteFood @idfood";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idfood });
            return result > 0;
        }
        public List<ListFood> SearchFood( string namefood)
        {
            List<ListFood> Listfood = new List<ListFood>();
            string query = "EXECUTE dbo.USP_getListFoodBySearching @nameFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query,new object[] { namefood});
            foreach (DataRow item in data.Rows)
            {
                ListFood food = new ListFood(item);
                Listfood.Add(food);
            }
            return Listfood;
        }
        public void DeleteFoodByIdCategory(int idcategory)
        {
            // xóa các billinfo có chứa các food có chứa id category này
            BillInFoDAO.Instance.DeleteBillInFoByIdCateogry(idcategory);
            string query = "EXECUTE dbo.USP_deleteFoodByIdCategory @idcategory";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idcategory });

        }
    }
}
