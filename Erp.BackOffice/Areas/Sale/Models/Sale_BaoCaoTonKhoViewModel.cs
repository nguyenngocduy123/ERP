
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class Sale_BaoCaoTonKhoViewModel
    {
        public string CategoryCode { get; set; }
        public string Manufacturer { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public decimal? Price { get; set; }
        public string ProductUnit { get; set; }
        public int? Quantity { get; set; }
        public string LoCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}