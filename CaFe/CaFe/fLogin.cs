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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // bắt sự kiện khi click vào "Đăng Nhập"
            string username = txbUserName.Text;
            string password = txbPassWord.Text;

            if (Login(username,password))
            {
                Account accountLogin = AccountDAO.Instance.getAccountByUserName(username);
                
                fTableManager f = new fTableManager(accountLogin); // khởi tạo cái form table
                this.Hide(); // ẩn cái form đăng nhập
                f.ShowDialog(); // cho xuất hiện form table kiểu dialog
                this.Show(); // hiện form đăng nhập khi form table tắt
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!!");
            }
           
        }
        bool Login(string username, string password)
        {

            return AccountDAO.Instance.Login(username, password);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // bắt sự kiện cho button "Thoát" 
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // bắt sự kiện khi đang đóng form
            if(MessageBox.Show("Bạn có muốn thật sự THOÁT?","Thông Báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true; // nếu không chọn OK thì cái MessageBox sẽ mất
            }
        }
    }
}
