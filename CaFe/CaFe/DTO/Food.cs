using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
    public class Food
    {
        private int id;
        public int Id { get => id; set => id = value; }
        

        private int idCategory;
        public int IdCategory { get => idCategory; set => idCategory = value; }
        

        private string nameFood;
        public string NameFood { get => nameFood; set => nameFood = value; }
        

        private double price;
        public double Price { get => price; set => price = value; }

        public Food(int id, int idCategory, string nameFood, double price)
        {
            Id = id;
            IdCategory = idCategory;
            NameFood = nameFood;
            Price = price;
        }

        public Food(DataRow row)
        {
            Id = (int)row["id"];
            IdCategory = (int)row["idCategory"];
            NameFood = row["name"].ToString();
            Price = (double)row["price"];
        }
    }
}
