namespace Erp.BackOffice.Areas.Sale.Views.SaleReport.Design
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for PaymentList.
    /// </summary>
    public partial class PaymentList : Telerik.Reporting.Report
    {
        public PaymentList()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //var Logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            //Image image1 = Image.FromFile(Logo);
            //this.pictureBox1.Value = image1;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //

        }
    }
}