using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class SaleReportSumaryViewModel
    {
        public bool IsFilter_ProductGroup { get; set; }
        public decimal? Revenue { get; set; }
        public int NumberOfProductInvoice { get; set; }
        public int NumberOfProductInvoice_Pendding { get; set; }
        public int NumberOfProductInvoice_InProgress { get; set; }
        public int NumberOfSaleOrder { get; set; }
        public decimal? SalesReturnAmount { get; set; }
        public int NumberOfSalesReturn { get; set; }
        public string ProductGroup { get; set; }
        public decimal? TotalFixedDiscount { get; set; }
        public decimal? TotalIrregularDiscount { get; set; }
        public int TotalDayInvoice { get; set; }
        public decimal? AmountCommissionStaff { get; set; }
        //public decimal? AmountWeek1 { get; set; }
        //public int? Week1 { get; set; }
        //public string Status1 { get; set; }
        //public decimal? AmountWeek2 { get; set; }
        //public int? Week2 { get; set; }
        //public string Status2 { get; set; }
        //public decimal? AmountWeek3 { get; set; }
        //public int? Week3 { get; set; }
        //public string Status3 { get; set; }
        //public decimal? AmountWeek4 { get; set; }
        //public int? Week4 { get; set; }
        //public string Status4 { get; set; }

        public int SoVIP { get; set; }
        public List<ProductInvoiceViewModel> ProductInvoiceList { get; set; }
    }
}