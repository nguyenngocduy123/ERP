using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class DayOffViewModel
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
        [Display(Name = "DayOffCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DayStart", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayStart { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DayEnd", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> DayEnd { get; set; }

        [Display(Name = "QuantityDayOff", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public Nullable<int> TypeDayOffId { get; set; }

        [Display(Name = "QuantityDayNotUsed", ResourceType = typeof(Wording))]
        public Nullable<int> QuantityNotUsed { get; set; }

        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }

        [Display(Name = "QuantityDayOff", ResourceType = typeof(Wording))]
        public Nullable<int> TypeDayOffQuantity { get; set; }
        //[Display(Name = "Pay", ResourceType = typeof(Wording))]
        //public Nullable<int> Pay { get; set; }

        [Display(Name = "TypeDayOffName", ResourceType = typeof(Wording))]
        public string NameSymbol { get; set; }

        public IEnumerable<SelectListItem> TypeList { get; set; }

        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string NameStaff { get; set; }

        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string CodeStaff { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public string DepartmentName { get; set; }
        [Display(Name = "Symbol", ResourceType = typeof(Wording))]
        public string CodeSymbol { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }

        public Nullable<int> BranchDepartmentId { get; set; }
        public string ProfileImage { get; set; }
    }
}