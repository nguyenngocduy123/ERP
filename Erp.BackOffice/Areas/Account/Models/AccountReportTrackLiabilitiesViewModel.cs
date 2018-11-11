using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class AccountReportTrackLiabilitiesViewModel
    {
        public decimal CongNoPhaiThuKhachHang { get; set; }
        public decimal CongNoPhaiTraNhaCungCap { get; set; }
    }
}