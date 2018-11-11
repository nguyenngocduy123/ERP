
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class Sale_BaoCaoXuatKhoViewModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryCode { get; set; }
        public string Manufacturer { get; set; }
        public string WarehouseName { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public int? invoice { get; set; }
        public int? service { get; set; }
        public int? _internal { get; set; }
        public string LoCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}