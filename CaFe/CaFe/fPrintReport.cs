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
    public partial class fPrintReport : Form
    {
        private ReportBill report;
        public ReportBill Report { get => report; set => report = value; }
        public fPrintReport(ReportBill report)
        {
            Report = report;
            InitializeComponent();
        }

        private void fPrintReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLyQuanCafeDataSet.USP_getReport' table. You can move, or remove it, as needed.
            this.USP_getReportTableAdapter.Fill(this.QuanLyQuanCafeDataSet.USP_getReport,Report.Id);

            // thêm giảm giá và tổng tiền
            Microsoft.Reporting.WinForms.ReportParameter[] rParams = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("giamgia", Report.Discount.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("tongtien", Report.Tongtien.ToString())
            };
            reportViewer1.LocalReport.SetParameters(rParams);
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }
    }
}
