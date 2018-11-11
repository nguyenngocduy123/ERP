using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.Domain.Staff.Entities;

namespace Erp.BackOffice.Staff.Models
{
    public class WelfareProgramsViewModel
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
        [Display(Name = "WelfareName", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "ProvideStartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ProvideStartDate { get; set; }
        [Display(Name = "ProvideEndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ProvideEndDate { get; set; }
        [Display(Name = "QuantityStaff", ResourceType = typeof(Wording))]
        public Nullable<int> Quantity { get; set; }
        [Display(Name = "TotalEstimatedCost", ResourceType = typeof(Wording))]
        public Nullable<int> TotalEstimatedCost { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        [Display(Name = "Purpose", ResourceType = typeof(Wording))]
        public string Purpose { get; set; }
        [Display(Name = "Formality", ResourceType = typeof(Wording))]
        public string Formality { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Wording))]
        public string Category { get; set; }
        [Display(Name = "RegistrationStartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> RegistrationStartDate { get; set; }
        [Display(Name = "RegistrationEndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> RegistrationEndDate { get; set; }
        [Display(Name = "ImplementationStartDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ImplementationStartDate { get; set; }
        [Display(Name = "ImplementationEndDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ImplementationEndDate { get; set; }
        [Display(Name = "MoneyStaff", ResourceType = typeof(Wording))]
        public Nullable<int> MoneyStaff { get; set; }
        [Display(Name = "MoneyCompany", ResourceType = typeof(Wording))]
        public Nullable<int> MoneyCompany { get; set; }
        [Display(Name = "TotalStaffCompany", ResourceType = typeof(Wording))]
        public Nullable<int> TotalStaffCompany { get; set; }
        [Display(Name = "TotalMoneyStaff", ResourceType = typeof(Wording))]
        public Nullable<int> TotalMoneyStaff { get; set; }
        [Display(Name = "TotalMoneyCompany", ResourceType = typeof(Wording))]
        public Nullable<int> TotalMoneyCompany { get; set; }
        [Display(Name = "TotalStaffCompanyAll", ResourceType = typeof(Wording))]
        public Nullable<int> TotalStaffCompanyAll { get; set; }
        [Display(Name = "TotalActualCosts", ResourceType = typeof(Wording))]
        public Nullable<int> TotalActualCosts { get; set; }
        [Display(Name = "ApplicationObject", ResourceType = typeof(Wording))]
        public string ApplicationObject { get; set; }
        [Display(Name = "WelfareCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        public List<WelfareProgramsDetailViewModel> ListStaff { get; set; }

    }
}