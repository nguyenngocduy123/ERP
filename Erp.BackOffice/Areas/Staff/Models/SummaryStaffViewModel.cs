using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class SummaryStaffViewModel
    {
        public string id { get; set; }
        public string label { get; set; }
        public decimal? Amount { get; set; }
        public int? Quantity { get; set; }

    }
}