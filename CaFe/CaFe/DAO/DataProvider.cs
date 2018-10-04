using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class DataProvider // lấy dữ liệu từ SQL
    {
        private static DataProvider instance; 
        public static DataProvider Instance // phương thức khởi tạo hay lấy biến toàn cục "instance"
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataProvider();
                }
                return DataProvider.instance;
            }
            private set
            {
                DataProvider.instance = value;
            }
        }
        private DataProvider()
        {

        }

        private string connectionString = @"Data Source =.\sqlexpress;Initial Catalog = QuanLyQuanCafe; Integrated Security = True"; 

        public DataTable ExecuteQuery(string query , object[] parameter = null) // trả ra dữ liệu
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // mở kết nối

                SqlCommand command = new SqlCommand(query, connection); // thực hiện truy vấn khi đã kết nối với SQL
                if(parameter != null)
                {
                    string[] listPara = query.Split(' '); // tách các từ trong query bởi các dấu cách
                    int i = 0;
                    foreach(string item in listPara)
                    {
                        if(item.Contains('@')) // kiếm các từ có @ => đó là parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++; 
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command); // lấy dữ liệu từ câu truy vấn


                adapter.Fill(data); // cho dữ liệu vào data

                connection.Close(); // đóng kết nối
            }


               

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null) // trả ra số dòng thành công cho tác vụ insert và update
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // mở kết nối

                SqlCommand command = new SqlCommand(query, connection); // thực hiện truy vấn khi đã kết nối với SQL
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); // tách các từ trong query bởi các dấu cách
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@')) // kiếm các từ có @ => đó là parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();

                connection.Close(); // đóng kết nối
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null) // trả ra số lượng khi dùng count*
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // mở kết nối

                SqlCommand command = new SqlCommand(query, connection); // thực hiện truy vấn khi đã kết nối với SQL
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); // tách các từ trong query bởi các dấu cách
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@')) // kiếm các từ có @ => đó là parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();

                connection.Close(); // đóng kết nối
            }

            return data;
        }

    }
}
