using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class BillInFoDAO
    {
        private static BillInFoDAO instance;
        public static BillInFoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInFoDAO();
                }
                return BillInFoDAO.instance;
            }
            private set
            {
                BillInFoDAO.instance = value;
            }
        }
        private BillInFoDAO()
        {

        }

        public List<BillInFo> getListBillInfo(int idBill)
        {
            List<BillInFo> listBillInFo = new List<BillInFo>();
            string query = "EXECUTE dbo.USP_getBillInFoByIDBill @idBill";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idBill });
            foreach(DataRow item in data.Rows)
            {
                BillInFo billinfo = new BillInFo(item);
                listBillInFo.Add(billinfo);
            }
            
            return listBillInFo;
        }

        public void insertBillInFo(int idBill , int idFood , int count)
        {
            string query = "EXECUTE dbo.USP_insertBillInFo @idBill , @idFood , @count";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood, count });
        }
        public void DeleteBillInFoByIdFood(int idfood)
        {
            string query = "EXECUTE dbo.USP_deleteBillInfoByidFood @idfood";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idfood });
        }
        public void DeleteBillInFoByIdCateogry( int idcategory)
        {
            string query = "EXECUTE dbo.USP_deleteBillInfoByIdCategory @idcategory";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idcategory });
        }
        public void DeleteBillInFoByIdTable(int idtable)
        {
            string query = "EXECUTE dbo.USP_deleteBillInfoByIdTable @idtable";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idtable });
        }
    }
}
