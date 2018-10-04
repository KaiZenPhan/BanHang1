using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
   public class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BillDAO();
                }
                return BillDAO.instance;
            }
            private set
            {
                BillDAO.instance = value;
            }
        }
        private BillDAO()
        {

        }

        public int getUnCheckOutBillIDByTableID(int id)
        {
            string query = "EXECUTE dbo.USP_getUnCheckOutBillIDByTableID @tableID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query,new object[] { id});
            
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1; // không có bill nào
        }

        public void insertBill(int idTable)
        {
            string query = "EXECUTE dbo.USP_insertBill @idTable ";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idTable});
        }
        public int getMaxIdBill()
        {
            string query = "EXECUTE dbo.USP_getMaxIdBill";
            try
            {
                int maxID = (int)DataProvider.Instance.ExecuteScalar(query);
                return maxID;
            }
            catch
            {
                return 1;
            }
            
            
        }
        public void CheckOutBill(int idBill , int discount , double sumprice)
        {
            string query = "EXECUTE dbo.USP_CheckOutBill @idBill , @discount , @sumprice";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill , discount , sumprice });
        }

        public DataTable getListBillCheckOut(DateTime start , DateTime end)
        {
            
            string query = "EXECUTE dbo.USP_getListBillCheckOut @dateStart  ,  @dateEnd";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { start, end });
            return data;
              

        }
        public void DeleteBillByIdTable(int idtable)
        {
            // xóa các billinfo có chứa id bill mà có chứa idtable
            string query = "EXECUTE dbo.USP_deleteBillByIdTable @idtable";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idtable });
        }

    }
}
