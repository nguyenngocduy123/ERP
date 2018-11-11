using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Account.Models
{
    public class ContactViewModel
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

        [Display(Name = "surrogate", ResourceType = typeof(Wording))]
        public string FullName { get; set; }

        [Display(Name = "Customer", ResourceType = typeof(Wording))]
        public Nullable<int> CustomerId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> Birthday { get; set; }
        [Display(Name = "Gender", ResourceType = typeof(Wording))]
        public Nullable<bool> Gender { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }
        [Display(Name = "WardName", ResourceType = typeof(Wording))]
        public string WardId { get; set; }
        [Display(Name = "DistrictName", ResourceType = typeof(Wording))]
        public string DistrictId { get; set; }
        [Display(Name = "CityName", ResourceType = typeof(Wording))]
        public string CityId { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }
        [Display(Name = "Mobile", ResourceType = typeof(Wording))]
        public string Mobile { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        public int? SupplierId { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public string DepartmentName { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string Position { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceName { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictName { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardName { get; set; }
        [Display(Name = "GenderName", ResourceType = typeof(Wording))]
        public string GenderName { get; set; }
    }
}