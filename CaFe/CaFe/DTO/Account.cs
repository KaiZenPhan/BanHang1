using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaFe.DTO
{
    public class Account
    {
        private string userName;
        public string UserName { get => userName; set => userName = value; }
        

        private string passWord;
        public string PassWord { get => passWord; set => passWord = value; }
        

        private string displayName;
        public string DisplayName { get => displayName; set => displayName = value; }
        

        private int type;
        public int Type { get => type; set => type = value; }

        public Account(string userName, string passWord, string displayName, int type)
        {
            UserName = userName;
            PassWord = passWord;
            DisplayName = displayName;
            Type = type;
        }

        public Account(DataRow row)
        {
            UserName = row["UserName"].ToString();
            PassWord = row["PassWord"].ToString();
            DisplayName = row["DisplayName"].ToString();
            Type = (int)row["Type"];
        }
    }
}
