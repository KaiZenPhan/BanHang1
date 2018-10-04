using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDAO();
                }
                return TableDAO.instance;
            }
            private set
            {
                TableDAO.instance = value;
            }
        }

        private TableDAO()
        {

        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            string query = "EXECUTE dbo.USP_getTableList";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;

        }
        
        public void SwitchTable (int idTable1 , int idTable2)
        {
            string query = "EXECUTE dbo.USP_SwitchTable @idTable1 , @idTable2";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTable1, idTable2 });
        }
        public bool AddTable(string name)
        {
            string query = "EXECUTE dbo.USP_addTable @nametable";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name });
            return result > 0;
        }
        public bool UpdateTable(int id , string name)
        {
            string query = "EXECUTE dbo.USP_updateTable @id , @nametable  ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {id ,name  });
            return result > 0;
        }
        public bool DeleteTable(int id)
        {
            // xóa Bill trước vì Bill có chứa id của bàn cần xóa
            BillDAO.Instance.DeleteBillByIdTable(id);
            string query = "EXECUTE dbo.USP_deleteTable @idtable";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }
        public List<Table> SearchTable(string name)
        {
            List<Table> tableList = new List<Table>();
            string query = "EXECUTE dbo.USP_getListTableBySearching @name";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { name});

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
    }
}
