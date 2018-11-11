using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Areas.Staff.Models;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class CommisionEditViewModel
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public List<StaffGeneralInfoViewModel> ListStaff { get; set; }
    }
}