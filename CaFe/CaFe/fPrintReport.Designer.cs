namespace CaFe
{
    partial class fPrintReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.USP_getReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QuanLyQuanCafeDataSet = new CaFe.QuanLyQuanCafeDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.USP_getReportTableAdapter = new CaFe.QuanLyQuanCafeDataSetTableAdapters.USP_getReportTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.USP_getReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuanLyQuanCafeDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // USP_getReportBindingSource
            // 
            this.USP_getReportBindingSource.DataMember = "USP_getReport";
            this.USP_getReportBindingSource.DataSource = this.QuanLyQuanCafeDataSet;
            // 
            // QuanLyQuanCafeDataSet
            // 
            this.QuanLyQuanCafeDataSet.DataSetName = "QuanLyQuanCafeDataSet";
            this.QuanLyQuanCafeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.USP_getReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CaFe.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 484);
            this.reportViewer1.TabIndex = 0;
            // 
            // USP_getReportTableAdapter
            // 
            this.USP_getReportTableAdapter.ClearBeforeFill = true;
            // 
            // fPrintReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.reportViewer1);
            this.Name = "fPrintReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fPrintReport";
            this.Load += new System.EventHandler(this.fPrintReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.USP_getReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuanLyQuanCafeDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource USP_getReportBindingSource;
        private QuanLyQuanCafeDataSet QuanLyQuanCafeDataSet;
        private QuanLyQuanCafeDataSetTableAdapters.USP_getReportTableAdapter USP_getReportTableAdapter;
    }
}