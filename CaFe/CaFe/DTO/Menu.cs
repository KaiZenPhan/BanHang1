using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
   public class Menu
    {
        private string foodName;
        public string FoodName { get => foodName; set => foodName = value; }
        

        private int count;
        public int Count { get => count; set => count = value; }

        private double price;
        public double Price { get => price; set => price = value; }

        private double totalPrice;
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        

        public Menu(string foodName, int count,double price, double totalPrice)
        {
            FoodName = foodName;
            Count = count;
            Price = price;
            TotalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            FoodName = row["name"].ToString();
            Count = (int)row["count"];
            Price = (double)row["price"];
            TotalPrice = (double)row["totalPrice"];
        }
    }
}
