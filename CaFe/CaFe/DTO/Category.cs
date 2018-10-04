using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
    public class Category
    {
        private int id;
        public int Id { get => id; set => id = value; }
        

        private string nameCategory;
        public string NameCategory { get => nameCategory; set => nameCategory = value; }

        public Category(int id, string nameCategory)
        {
            Id = id;
            NameCategory = nameCategory;
        }

        public Category(DataRow row)
        {
            Id = (int)row["id"];
            NameCategory = row["name"].ToString();
        }
    }
}
