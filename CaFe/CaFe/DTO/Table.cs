using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
    public class Table
    {
        public static int tableWidth = 100; // kích thước cửa mỗi bàn thông qua button
        public static int tableHeight = 100;
        private int id;
        public int Id { get => id; set => id = value; }

        private string name;
        public string Name { get => name; set => name = value; } // giống hàm getter và setter cú pháp : Ctrl + R + E
        

        private string status;
        public string Status { get => status; set => status = value; }

        public Table(int id , string name , string status) // hàm khởi tạo
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        public Table(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
    }
}
