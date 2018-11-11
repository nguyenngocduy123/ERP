using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class CheckInOutViewModel
    {
        [Display(Name = "UserEnrollNumber", ResourceType = typeof(Wording))]
        public Nullable<int> UserId { get; set; }
        [Display(Name = "TimeDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> TimeDate { get; set; }
        [Display(Name = "TimeStr", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> TimeStr { get; set; }
        [Display(Name = "TimeType", ResourceType = typeof(Wording))]
        public string TimeType { get; set; }
        [Display(Name = "TimeSource", ResourceType = typeof(Wording))]
        public string TimeSource { get; set; }
        [Display(Name = "MachineNo", ResourceType = typeof(Wording))]
        public Nullable<int> MachineNo { get; set; }
        [Display(Name = "CardNo", ResourceType = typeof(Wording))]
        public string CardNo { get; set; }
        [Display(Name = "AutoID", ResourceType = typeof(Wording))]
        public int Id { get; set; }
        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> FPMachineId { get; set; }
    }
}