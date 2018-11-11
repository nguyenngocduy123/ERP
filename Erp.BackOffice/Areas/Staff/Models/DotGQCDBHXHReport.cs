using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class DotGQCDBHXHReport
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "BatchNumber", ResourceType = typeof(Wording))]
        public Nullable<int> BatchNumber { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }

        // detail
        [Display(Name = "DotGQCDBHXHId", ResourceType = typeof(Wording))]
        public Nullable<int> DotGQCDBHXHId { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "DayOffId", ResourceType = typeof(Wording))]
        public Nullable<int> DayOffId { get; set; }
        [Display(Name = "SocietyCode", ResourceType = typeof(Wording))]
        public string SocietyCode { get; set; }
        [Display(Name = "DKTH_TinhTrang", ResourceType = typeof(Wording))]
        public string DKTH_TinhTrang { get; set; }
        [Display(Name = "DKTH_ThoiDiem", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DKTH_ThoiDiem { get; set; }
        [Display(Name = "DayStart", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayStart { get; set; }
        [Display(Name = "DayEnd", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayEnd { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }
        [Display(Name = "PaymentMethod", ResourceType = typeof(Wording))]
        public string PaymentMethod { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "TypeDayOffName", ResourceType = typeof(Wording))]
        public string DayOffName { get; set; }

        [Display(Name = "Symbol", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }
    }
}