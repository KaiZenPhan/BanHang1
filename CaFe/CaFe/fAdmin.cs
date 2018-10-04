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
    public partial class fAdmin : Form
    {
        public Account accountLogin;
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
        #region Method
        void Load()
        {
            dtgvFood.DataSource = foodList;
            dtgvlistAccount.DataSource = accountList;
            dtgvTable.DataSource = tableList;
            dtgvCategory.DataSource = categoryList;


            LoadDateTimePicker();
            LoadListBillCheckOut(dtpkFromDay.Value, dtpkToDay.Value);

            LoadListFood();
            AddFoodBinding();
            LoadCategoryFood(cbFoodCategory);

            LoadListAccount();
            AddAccountBinding();
            LoadListTypeAccount(cbAccountType);

            LoadListTable();
            AddTableBinding();

            LoadListCategory();
            AddCategoryBinding();

        }
        //tab Hóa Đơn
        public void LoadListBillCheckOut(DateTime star , DateTime end)
        {
            dtgvBill.DataSource = BillDAO.Instance.getListBillCheckOut(star, end);
        }
        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpkFromDay.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDay.Value = dtpkFromDay.Value.AddMonths(1).AddDays(-1);
        }
        /*================================================*/

         //Tab Food
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.getListFood();
        }
        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id",true,DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "NameFood", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "NameCategory", true, DataSourceUpdateMode.Never));
            numFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryFood(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.getListCategory();
            cb.DisplayMember = "NameCategory";
        }
        List<ListFood> SearchFood(string name)
        {
            List<ListFood> listFoodBySearch = FoodDAO.Instance.SearchFood(name) ;
            return listFoodBySearch;
        }
        /*================================================*/

        //Tab Account
        void LoadListAccount()
        {
            accountList.DataSource = AccountDAO.Instance.getListAccount();
        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvlistAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvlistAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            cbAccountType.DataBindings.Add(new Binding("Text", dtgvlistAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadListTypeAccount(ComboBox cb)
        {
            cb.DataSource = AccountDAO.Instance.getListAccount();
            cb.DisplayMember = "Type";
        }

        /*================================================*/

        //Tab Bàn
        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
        }
        void AddTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        List<Table> SearchTable(string name)
        {
            List<Table> listTableBySearch = TableDAO.Instance.SearchTable(name);
            return listTableBySearch;
        }


        /*================================================*/

        //Tab Danh Mục
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.getListCategory();
        }
        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "NameCategory", true, DataSourceUpdateMode.Never));
        }
        List<Category> SearchCategory(string name)
        {
            List<Category> listCategoryBySearch = CategoryDAO.Instance.SearchCategory(name);
            return listCategoryBySearch;
        }
        /*================================================*/
        #endregion

        /* Trên là method Dưới là Event */


        #region Events
        // tab Hóa đơn
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillCheckOut(dtpkFromDay.Value , dtpkToDay.Value);
        }
        /*================================================*/


        //Tab Food
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idcategory = (cbFoodCategory.SelectedItem as Category).Id;
            double price = (double)numFoodPrice.Value;
            if(FoodDAO.Instance.insertFood(name , idcategory,price))
            {
                MessageBox.Show("Thêm Thành Công");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
        }
        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse( txbFoodID.Text);
            string name = txbFoodName.Text;
            int idcategory = (cbFoodCategory.SelectedItem as Category).Id;
            double price = (double)numFoodPrice.Value;
            if (FoodDAO.Instance.updateFood(id,name, idcategory, price))
            {
                MessageBox.Show("Sửa Thành Công");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbFoodID.Text);
            
            if (FoodDAO.Instance.deleteFood(id))
            {
                MessageBox.Show("Xóa Thành Công");
                LoadListFood();
                if(deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string name = txbSearchNameFood.Text;
           foodList.DataSource =  SearchFood(name);
        }

        /*================================================*/

        //Tab Account
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            int kiemtra = 0;
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = (cbAccountType.SelectedItem as Account).Type;
            // kiểm tra xem có bị trùng username k ?
            List<Account> listacc = AccountDAO.Instance.getListAccount();
            foreach(Account item in listacc)
            {
                if(item.UserName.Equals(username))
                {
                    
                    kiemtra = 1; // là bị trùng
                    break;
                }
            }
            if(kiemtra == 0)
            {
                if (AccountDAO.Instance.AddAccount(username, displayname, type))
                {
                    MessageBox.Show("Thêm Thành Công");
                    LoadListAccount();
                }
                else
                {
                    MessageBox.Show("Xem lại phần UserName");
                }
            }
            else
            {
                MessageBox.Show("Tên Tài Khoản Bị Trùng!! Vui Lòng Chọn UserName Khác");
            }
            
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = (cbAccountType.SelectedItem as Account).Type;
            if (AccountDAO.Instance.UpdateAccount(username, displayname, type))
            {
                MessageBox.Show("Sửa Thành Công");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            int kiemtra = 0;
            string username = txbUserName.Text;
            if(username.Equals(accountLogin.UserName))
            {
                kiemtra = 1;
            }
            if (kiemtra == 0)
            {
                if (AccountDAO.Instance.DeleteAccount(username))
                {
                    MessageBox.Show("Xóa Thành Công");
                    LoadListAccount();
                }
                else
                {
                    MessageBox.Show("Xóa Thất Bại");
                }
            }
            else
            {
                MessageBox.Show("Tài Khoản Đang Đăng Nhập");
            }
            
        }
        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            if (AccountDAO.Instance.DatLaiMK(username))
            {
                MessageBox.Show("Đặt Lại MK Thành Công");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Đặt Lại MK Thất Bại");
            }

        }

        /*================================================*/

        //Tab Bàn
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            
            string name = txbTableName.Text;
            if(TableDAO.Instance.AddTable(name))
            {
                MessageBox.Show("Thêm Bàn Thành Công");
                LoadListTable();
                if (insertTable != null)
                {
                    insertTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Thêm Bàn Thất Bại");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = int.Parse( txbTableID.Text);
            string name = txbTableName.Text;

            if (TableDAO.Instance.UpdateTable(id,name))
            {
                MessageBox.Show("Sửa Bàn Thành Công");
                LoadListTable();
                if (updateTable != null)
                {
                    updateTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Sửa Bàn Thất Bại");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbTableID.Text);
            

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa Bàn Thành Công");
                LoadListTable();
                if (deleteTable != null)
                {
                    deleteTable(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Xóa Bàn Thất Bại");
            }
        }

        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }

        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }
        private void btnSearchTable_Click(object sender, EventArgs e)
        {
            string name = txbSearchTableName.Text;
            tableList.DataSource = SearchTable(name);
        }

        /*================================================*/

        //Tab Danh Mục
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string namecategory = txbCategoryName.Text;
            if (CategoryDAO.Instance.AddCategory(namecategory))
            {
                MessageBox.Show("Thêm Thành Công");
                LoadListCategory();
                LoadCategoryFood(cbFoodCategory);
                if (insertCategory != null)
                {
                    insertCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse( txbCategoryID.Text);
            string namecategory = txbCategoryName.Text;
            if (CategoryDAO.Instance.UpdateCategory(id,namecategory ))
            {
                MessageBox.Show("Sửa Thành Công");
                LoadListCategory();
                LoadCategoryFood(cbFoodCategory);
                if (updateCategory != null)
                {
                    updateCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbCategoryID.Text);
            
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa Thành Công");
                LoadListCategory();
                LoadCategoryFood(cbFoodCategory);
                if (deleteCategory != null)
                {
                    deleteCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại");
            }
        }

        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            string name = txbSearchCategoryName.Text;
            categoryList.DataSource = SearchCategory(name);
        }
        /*================================================*/

        #endregion


    }
}
