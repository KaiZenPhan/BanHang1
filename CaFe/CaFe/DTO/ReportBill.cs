using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
    public class ReportBill
    {
        private int id;
        public int Id { get => id; set => id = value; }
        

        private double tongtien;
        public double Tongtien { get => tongtien; set => tongtien = value; }
        

        private int discount;
        public int Discount { get => discount; set => discount = value; }

        public ReportBill(int id, double tongtien, int discount)
        {
            Id = id;
            Tongtien = tongtien;
            Discount = discount;
        }


    }
}
