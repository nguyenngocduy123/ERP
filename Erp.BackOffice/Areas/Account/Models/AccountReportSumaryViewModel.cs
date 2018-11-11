using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class AccountReportSumaryViewModel
    {
        public double DoanhSo { get; set; }
        public double Thu { get; set; }
        public double Chi { get; set; }
        public double TienMat { get; set; }
        public double ChuyenKhoan { get; set; }
    }
}