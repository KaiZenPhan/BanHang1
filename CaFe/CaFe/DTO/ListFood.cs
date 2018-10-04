using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
   public class ListFood
    {
        private int id;
        public int Id { get => id; set => id = value; }
        

        private string nameFood;
        public string NameFood { get => nameFood; set => nameFood = value; }
        

        private string nameCategory;
        public string NameCategory { get => nameCategory; set => nameCategory = value; }
        

        private double price;
        public double Price { get => price; set => price = value; }

        public ListFood(int id, string nameFood, string nameCategory, double price)
        {
            Id = id;
            NameFood = nameFood;
            NameCategory = nameCategory;
            Price = price;
        }

        public ListFood(DataRow row)
        {
            Id = (int)row["id"];
            NameFood = row["name"].ToString();
            NameCategory = row["namecategory"].ToString();
            Price = (double)row["price"];
        }
    }
}
