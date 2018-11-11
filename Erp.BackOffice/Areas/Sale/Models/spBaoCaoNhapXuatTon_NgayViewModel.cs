using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class spBaoCaoNhapXuatTon_TuanViewModel
    {
        public string CategoryCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public Nullable<int> ProductMinInventory { get; set; }
        public Nullable<int> First_InboundQuantity { get; set; }
        public Nullable<int> First_OutboundQuantity { get; set; }
        public Nullable<int> First_Remain { get; set; }
        public Nullable<int> Last_InboundQuantity { get; set; }
        public Nullable<int> Last_OutboundQuantity { get; set; }
        public Nullable<int> Remain { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string LoCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string WarehouseName { get; set; }
        public Nullable<int> WarehouseId { get; set; }
    }
}