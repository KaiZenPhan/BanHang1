using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
    public class Bill
    {
        private int id;
        public int Id { get => id; set => id = value; }
        

        private DateTime? dateCheckIn;
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        

        private DateTime? dateCheckOut;
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        

        private int idTable;
        public int IdTable { get => idTable; set => idTable = value; }
        

        private int statusBill;
        public int StatusBill { get => statusBill; set => statusBill = value; }
        

        private int discount;
        public int Discount { get => discount; set => discount = value; }
        

        private double sumprice;
        public double Sumprice { get => sumprice; set => sumprice = value; }

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int idTable, int statusBill, int discount, double sumprice)
        {
            Id = id;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            IdTable = idTable;
            StatusBill = statusBill;
            Discount = discount;
            Sumprice = sumprice;
        }

        public Bill(DataRow row)
        {
            Id = (int)row["id"];
            DateCheckIn = (DateTime?)row["DateCheckIn"];

            var DateCheckOutTemp = row["DateCheckOut"];
            if(DateCheckOutTemp.ToString() != "")
            {
                DateCheckOut = (DateTime?)DateCheckOutTemp;
            }
            
            IdTable = (int)row["idTable"];
            StatusBill = (int)row["status"];
            Discount = (int)row["discount"];
            Sumprice = (double)row["sumprice"];
        }
    }
}
