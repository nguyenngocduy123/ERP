
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class Sale_BaoCaoNhapXuatTonViewModel
    {
        public string CategoryCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public string Manufacturer { get; set; }
        public decimal? PriceInbound { get; set; }
        public decimal? PriceOutbound { get; set; }
        public string ProductUnit { get; set; }
        public int? First_Remain { get; set; }
        public int? Last_InboundQuantity { get; set; }
        public int? Last_OutboundQuantity { get; set; }
        public int? Remain { get; set; }
        public string LoCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}