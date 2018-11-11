using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class InfoPartyAViewModel
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
        [Display(Name = "surrogate", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "NamePrefix", ResourceType = typeof(Wording))]
        public string NamePrefix { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> Birthday { get; set; }
        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string Position { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }
        [Display(Name = "IdCardNumber", ResourceType = typeof(Wording))]
        public string IdCardNumber { get; set; }
        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string IdCardIssued { get; set; }
        [Display(Name = "IdCardDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> IdCardDate { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }
        [Display(Name = "Fax", ResourceType = typeof(Wording))]
        public string Fax { get; set; }
        [Display(Name = "AccountNumber", ResourceType = typeof(Wording))]
        public string AccountNumber { get; set; }
        [Display(Name = "Bank", ResourceType = typeof(Wording))]
        public string Bank { get; set; }
        [Display(Name = "TaxCode", ResourceType = typeof(Wording))]
        public string TaxCode { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string CardIssuedName { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceName { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictName { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardName { get; set; }
        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceId { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictId { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardId { get; set; }
        [Display(Name = "CompanyName", ResourceType = typeof(Wording))]
        public string CompanyName { get; set; }
    }
}