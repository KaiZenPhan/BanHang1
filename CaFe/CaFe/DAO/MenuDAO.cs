using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new MenuDAO();

                }
                return MenuDAO.instance;
            
            }
            private set
            {
                MenuDAO.instance = value;
            }

        }
        private MenuDAO()
        {

        }

        public List<Menu> getListMenuByTableID (int idTable)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "EXECUTE dbo.USP_getMenuByIdTable @idTable";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idTable });
            foreach(DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
