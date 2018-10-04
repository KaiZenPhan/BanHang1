using CaFe.DAO;
using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaFe
{
   
    public partial class fAccountProfile : Form
    {
        private Account account;
        public Account Account { get => account; set => account = value; }
        public fAccountProfile(Account acc)
        {
            Account = acc;
            InitializeComponent();
            LoadInfoAccount(Account);
        }
        #region Method
        void LoadInfoAccount(Account acc)
        {
            txbDisplayName.Text = acc.DisplayName;
            txtUserName.Text = acc.UserName;
           
        }
        void UpdateAccount()
        {
            string displayname = txbDisplayName.Text;
            string username = txtUserName.Text;
            string pass = txbPassWord.Text;
            
            string newpass = txbNewPassWord.Text;
            string renewpass = txbReNewPassWord.Text;

            if(!newpass.Equals(renewpass))
            {
                MessageBox.Show("Nhập lại mật khẩu không đúng!!");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(username,displayname,pass,renewpass))
                {
                    MessageBox.Show("Cập Nhật Thành Công");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                }
            }
        }

        #endregion

        #region Events
        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
        #endregion


    }
}
