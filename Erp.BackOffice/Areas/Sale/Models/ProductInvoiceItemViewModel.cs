using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ProductInvoiceItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal? PriceInbound { get; set; }
        public decimal? PriceOutbound { get; set; }
        public string Type { get; set; }
        public string CategoryCode { get; set; }
        public bool? IsCombo { get; set; }
        public string Image_Name { get; set; }
        public int InventoryQuantity { get; set; }
        public string ProductGroup { get; set; }
        public string Manufacturer { get; set; }
        public string LoCode { get; set; }
        public string ExpiryDate { get; set; }
    }
}