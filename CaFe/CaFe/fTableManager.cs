using CaFe.DAO;
using CaFe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = CaFe.DTO.Menu;

namespace CaFe
{
    public partial class fTableManager : Form
    {
        private Account accountLogin;
        public Account AccountLogin { get => accountLogin; set => accountLogin = value; }
        
        public fTableManager(Account acc)
        {
            InitializeComponent();
            AccountLogin = acc;
            ChangeAccount(AccountLogin.Type);
            LoadTable();
            LoadCategory();
            LoadComboboxTable();
        }
        #region Method
        void ChangeAccount(int type)
        {
            
            if(type == 1)
            {
                adminToolStripMenuItem.Enabled = true;
            }
            else
            {
                adminToolStripMenuItem.Enabled = false;
            }
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + AccountLogin.DisplayName + ")";
        }
         void LoadTable() // hiển thị tất cả các bàn đang có
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach(Table item in tableList )
            {
                Button btn = new Button() { Width = Table.tableHeight , Height = Table.tableWidth };
                
                btn.Text = item.Name + Environment.NewLine + item.Status; // Enviroment.NewLine là \n
                btn.Click += btn_Click;

                btn.Tag = item; // gắn Tag cho btn sẽ đc gửi theo sender

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.getListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "NameCategory"; // hiển thị trường tên của category
        }
        void LoadFoodByCategoryId(int idCategory)
        {
            List<Food> listFood = FoodDAO.Instance.getListFoodByCategoryId(idCategory);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "NameFood";
        }
        double tongtien = 0;

        

        void ShowBillInfo(int idTable)
        {
            lvBillInFo.Items.Clear();

           
            List<Menu> listMenuInFo = MenuDAO.Instance.getListMenuByTableID(idTable);
            double sumPrice = 0;
            foreach(Menu item in listMenuInFo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString()); // tên món
                lsvItem.SubItems.Add(item.Count.ToString()); // số lượng            
                lsvItem.SubItems.Add(item.Price.ToString()); // giá 
                lsvItem.SubItems.Add(item.TotalPrice.ToString()); // thành tiền
                sumPrice += item.TotalPrice;
                lvBillInFo.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            tongtien = sumPrice;
            txbSumPrice.Text = sumPrice.ToString("c", culture); // đổi qua định đạng tiền theo biến culture || "c" là cú pháp : current
            string tien = txbSumPrice.Text.Split(',')[0];
            txbSumPrice.Text = tien + " Đ";


        }
        void LoadComboboxTable()
        {
            cbSwitchTable.DataSource = TableDAO.Instance.LoadTableList();
            cbSwitchTable.DisplayMember = "Name";
        }

        #endregion

        #region Events
        private void btn_Click(object sender, EventArgs e)
        {
            
            int idTable = ((sender as Button).Tag as Table).Id ; // sender là 1 object nên sẽ cho nó kiểu Table với điều kiện phải gán Tag

            lvBillInFo.Tag = (sender as Button).Tag; // thành phần của table 

            lblNameTable.Text = ((sender as Button).Tag as Table).Name;
            ShowBillInfo(idTable);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // bắt sự kiện khi chọn Đăng Xuất
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // bắt sự kiện khi chọn Thông tin cá nhân
            fAccountProfile f = new fAccountProfile(AccountLogin);
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // bắt sự kiện khi chọn Admin
            fAdmin f = new fAdmin();
            
            f.accountLogin = AccountLogin;

            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;

            f.InsertTable += f_InsertTable;
            f.DeleteTable += f_DeleteTable;
            f.UpdateTable += f_UpdateTable;

            f.InsertCategory += f_InsertCategory;
            f.DeleteCategory += f_DeleteCategory;
            f.UpdateCategory += f_UpdateCategory;

            f.ShowDialog();
        }

        private void f_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void f_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategory();
            LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
            if(lvBillInFo.Tag !=null )
            {
                ShowBillInfo((lvBillInFo.Tag as Table).Id);
            }
        }

        private void f_InsertCategory(object sender, EventArgs e)
        {
            LoadCategory();
            LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
        }

        private void f_UpdateTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void f_DeleteTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void f_InsertTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
            if (lvBillInFo.Tag != null)
            {
                ShowBillInfo((lvBillInFo.Tag as Table).Id);
            }
            
        }

        private void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
            if (lvBillInFo.Tag != null)
            {
                ShowBillInfo((lvBillInFo.Tag as Table).Id);
            }

        }

        private void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryId((cbCategory.SelectedItem as Category).Id);
            if (lvBillInFo.Tag != null)
            {
                ShowBillInfo((lvBillInFo.Tag as Table).Id);
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // bắt sự kiện khi chọn item trong combox
            int idCategory = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            idCategory = selected.Id;

            LoadFoodByCategoryId(idCategory); // khi chọn 1 cái category thì nó sẽ load danh sách món thuộc category đó
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lvBillInFo.Tag as Table;
            if (table == null)
            {
                if (MessageBox.Show("Chưa chọn bàn", "Thông báo", MessageBoxButtons.OK) == System.Windows.Forms.DialogResult.OK)
                {

                }
            }
            else
            {
                int idTableUnCheckOut = table.Id;


                int idBill = BillDAO.Instance.getUnCheckOutBillIDByTableID(idTableUnCheckOut);
                int idfood = (cbFood.SelectedItem as Food).Id;
                int count = (int)numFoodCount.Value;

                if (idBill == -1) // bàn đó chưa có bill
                {
                    BillDAO.Instance.insertBill(idTableUnCheckOut);

                    int idMaxOfBill = BillDAO.Instance.getMaxIdBill();
                    BillInFoDAO.Instance.insertBillInFo(idMaxOfBill, idfood, count);
                }
                else
                {
                    BillInFoDAO.Instance.insertBillInFo(idBill, idfood, count);
                }

                ShowBillInfo(idTableUnCheckOut);
                LoadTable();
            }
            
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lvBillInFo.Tag as Table;
            if(table == null)
            {
                if (MessageBox.Show("Chưa chọn bàn", "Thông báo", MessageBoxButtons.OK) == System.Windows.Forms.DialogResult.OK)
                {

                }
            }
            else
            {
                int idTableUnCheckOut = table.Id;
                int idBill = BillDAO.Instance.getUnCheckOutBillIDByTableID(idTableUnCheckOut);

                int discount = (int)numDisCount.Value;
                double sumprice_after_discount = tongtien * (100 - discount) / 100;

                CultureInfo culture = new CultureInfo("vi-VN");
                txbSumPrice.Text = sumprice_after_discount.ToString("c", culture);
                string tien = txbSumPrice.Text.Split(',')[0];
                txbSumPrice.Text = tien + " Đ";
                if (idBill != -1)
                {
                    if (MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho  " + table.Name + "\n\t\t" + tien + " Đồng" + "\n\t\t( ĐÃ giảm giá : " + discount.ToString() + "% )", "Thông Báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {

                        BillDAO.Instance.CheckOutBill(idBill, discount, sumprice_after_discount);
                        // tiến hành in bill ra và truyền các tham số : tổng tiền , giảm giá ,  id
                        ReportBill repo = new ReportBill(idBill, sumprice_after_discount, discount);
                        fPrintReport f = new fPrintReport(repo);
                        f.ShowDialog();
                        //
                        ShowBillInfo(idTableUnCheckOut);
                        LoadTable();
                    }
                    else
                    {
                        sumprice_after_discount = tongtien;
                        numDisCount.Value = 0;
                        txbSumPrice.Text = sumprice_after_discount.ToString("c", culture);
                        tien = txbSumPrice.Text.Split(',')[0];
                        txbSumPrice.Text = tien + " Đ";
                    }

                }
            }
            

        }
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table table1 = (lvBillInFo.Tag as Table);
            Table table2 = (cbSwitchTable.SelectedItem as Table);
            int idtable1 = table1.Id;
            int idtable2 = table2.Id;

            if(MessageBox.Show("Bạn muốn chuyển đến " + table2.Name,"Thông Báo",MessageBoxButtons.OKCancel)== System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(idtable1, idtable2);
                ShowBillInfo(idtable1);
                LoadTable();
                
            }
            else
            {

            }

        }

        #endregion


    }
}
