
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class Sale_BaoCaoCongNoTongHopViewModel
    {
        public string MaChungTuGoc { get; set; }
        public string CustomerName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? cnt { get; set; }
        public DateTime? NextPaymentDate { get; set; }
        public int? daytra { get; set; }
        public string SalerFullName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}