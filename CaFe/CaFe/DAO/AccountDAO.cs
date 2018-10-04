using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return AccountDAO.instance;
            }
            private set
            {
                AccountDAO.instance = value;
            }
        }
        private AccountDAO()
        {

        }

        public bool Login(string username , string password)
        {
            string query = "EXECUTE dbo.USP_Login @userName , @passWord ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return data.Rows.Count > 0;
        }
        public Account getAccountByUserName(string username)
        {
            string query = "EXECUTE dbo.USP_GetAccountByUserName @userName";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                return account;
            }
            return null; 
        }
        public bool UpdateAccount(string username , string displayname , string pass , string newpass)
        {
            string query = "EXECUTE dbo.USP_UpdateAccount @userName , @displayName , @password , @newpassword";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { username, displayname, pass, newpass });
            return result > 0;
        }

        public List<Account> getListAccount()
        {
            List<Account> listAccount = new List<Account>();
            string query = "EXECUTE dbo.USP_getListAccount";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                listAccount.Add(account);
            }
            return listAccount;
        }

        public bool AddAccount(string username , string displayname , int type)
        {
            string query = "EXECUTE dbo.USP_addAccount @username , @displayname , @type";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {username , displayname , type });
            return result > 0;

        }
        public bool UpdateAccount(string username, string displayname, int type)
        {
            string query = "EXECUTE dbo.USP_updateAccountByAdmin @username , @displayname , @type";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { username, displayname, type });
            return result > 0;

        }
        public bool DeleteAccount(string username)
        {
            string query = "EXECUTE dbo.USP_deleteAccount @username";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { username });
            return result > 0;
        }
        public bool DatLaiMK(string username)
        {
            string query = "EXECUTE dbo.USP_datlaiMk @username";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { username });
            return result > 0;
        }
    }
}
