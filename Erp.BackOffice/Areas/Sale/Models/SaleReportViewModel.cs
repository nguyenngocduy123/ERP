using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ChartItem
    {
        public string group { get; set; }
        public string label { get; set; }
        public string label2 { get; set; }
        public object data { get; set; }
        public object data2 { get; set; }
        public string id { get; set; }
    }
}