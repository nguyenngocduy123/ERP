using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ChartInboundAndOutboundInMonthViewModel
    {
        public string jsonInbound { get; set; }
        public string jsonOutbound { get; set; }
        public string jsonCategory { get; set; }
        public int TongNhap { get; set; }
        public int TongXuat { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public string Quarter { get; set; }
        public int Year { get; set; }
        public string Single { get; set; }
        public DataTable GroupList { get; set; }
    }
}