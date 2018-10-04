using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
   public class BillInFo
    {
        private int id;
        public int Id { get => id; set => id = value; }
        

        private int idBill;
        public int IdBill { get => idBill; set => idBill = value; }
        

        private int idFood;
        public int IdFood { get => idFood; set => idFood = value; }
        

        private int count;
        public int Count { get => count; set => count = value; }

        public BillInFo(int id, int idBill, int idFood, int count)
        {
            Id = id;
            IdBill = idBill;
            IdFood = idFood;
            Count = count;
        }

        public BillInFo(DataRow row)
        {
            Id = (int)row["id"];
            IdBill = (int)row["idBill"];
            IdFood = (int)row["idFood"];
            Count = (int)row["count"];
        }
    }
}
