using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Staff.Models;
namespace Erp.BackOffice.Account.Models
{
    public class CustomerViewModel
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

        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Wording))]
        public string FullName { get; set; }

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

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "Mobile", ResourceType = typeof(Wording))]
        public string Mobile { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "CustomerName", ResourceType = typeof(Wording))]
        public string CompanyName { get; set; }

        [Display(Name = "BonusScore", ResourceType = typeof(Wording))]
        public int? Point { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceName { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictName { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardName { get; set; }

        [Display(Name = "GenderName", ResourceType = typeof(Wording))]
        public string GenderName { get; set; }

        public List<ObjectAttributeValueViewModel> AttributeValueList { get; set; }

        [Display(Name = "CardCodeCustomer", ResourceType = typeof(Wording))]
        public string CardCode { get; set; }

        public string SearchFullName { get; set; }

        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string Image { get; set; }
        public string Image_Path { get; set; }
        public string Image_File { get; set; }
        [Display(Name = "IdCardNumber", ResourceType = typeof(Wording))]
        public string IdCardNumber { get; set; }

        [Display(Name = "IdCardDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> IdCardDate { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string IdCardIssued { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string CardIssuedName { get; set; }
        public decimal Liabilities { get; set; }
        public IEnumerable<SelectListItem> ProvinceList { get; set; }
        public string CustomerType { get; set; }
        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public int? BranchId { get; set; }
        public int ManagerStaffId { get; set; }
    }
}