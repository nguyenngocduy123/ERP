using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Areas.Staff.Models;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class CommisionDetailViewModel
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public List<CommisionViewModel> DetailList { get; set; }
    }
}