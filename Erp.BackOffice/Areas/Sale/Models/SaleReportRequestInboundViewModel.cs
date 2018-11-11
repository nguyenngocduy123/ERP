using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class SaleReportRequestInboundViewModel
    {
        public int NumberOfRequestInbound { get; set; }
        public int NumberOfRequestInbound_new { get; set; }
        public int NumberOfRequestInbound_InProgress { get; set; }
        public int NumberOfRequestInbound_wait_shipping { get; set; }
        public int NumberOfRequestInbound_shipping { get; set; }
        public int NumberOfRequestInbound_inbound_complete { get; set; }
        public int NumberOfRequestInbound_refure { get; set; }
        public int NumberOfRequestInbound_Error_success { get; set; }
        public int NumberOfRequestInbound_Error { get; set; }
        public int NumberOfRequestInbound_Error_no_success { get; set; }
    }
}