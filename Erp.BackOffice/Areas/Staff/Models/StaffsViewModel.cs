using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Erp.Domain.Entities;
using System.Web.Mvc;



namespace Erp.BackOffice.Staff.Models
{
    public class StaffsViewModel
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

        [StringLength(50, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        [DataType(DataType.Date, ErrorMessageResourceName = "DateInvalid", ErrorMessageResourceType = typeof(Error))]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        public Nullable<bool> Gender { get; set; }

        [StringLength(200, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError")]
        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "IdCardNumber", ResourceType = typeof(Wording))]
        public string IdCardNumber { get; set; }

        [Display(Name = "IdCardDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> IdCardDate { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string IdCardIssued { get; set; }

        [Display(Name = "Ethnic", ResourceType = typeof(Wording))]
        public string Ethnic { get; set; }

        [Display(Name = "Religion", ResourceType = typeof(Wording))]
        public string Religion { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Email", ResourceType = typeof(Wording))]
        [EmailAddress(ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "EmailError", ErrorMessage = null)]
        public string Email { get; set; }

        [Display(Name = "Literacy", ResourceType = typeof(Wording))]
        public string Literacy { get; set; }

        [Display(Name = "Technique", ResourceType = typeof(Wording))]
        public string Technique { get; set; }

        [Display(Name = "LanguageLevel", ResourceType = typeof(Wording))]
        public string Language { get; set; }

        //[Display(Name = "Department", ResourceType = typeof(Wording))]
        //public Nullable<int> BranchDepartmentId { get; set; }

        //[Display(Name = "BranchName", ResourceType = typeof(Wording))]
        //public Nullable<int> BranchId { get; set; }

        //[Display(Name = "Department", ResourceType = typeof(Wording))]
        //public string Staff_DepartmentId { get; set; }

        //[Display(Name = "Position", ResourceType = typeof(Wording))]
        //public string Position { get; set; }

        //[Display(Name = "Position", ResourceType = typeof(Wording))]
        //public string PositionName { get; set; }

        [Display(Name = "MaritalStatus", ResourceType = typeof(Wording))]
        public string MaritalStatus { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceId { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictId { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardId { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[Display(Name = "BranchName", ResourceType = typeof(Wording))]
        //public Nullable<int> Sale_BranchId { get; set; }

        [Display(Name = "ProfileImagePath", ResourceType = typeof(Wording))]
        public string ProfileImagePath { get; set; }
        public string ProfileImageFile { get; set; }

        [Display(Name = "ProfileImageName", ResourceType = typeof(Wording))]
        public string ProfileImageCurrent { get; set; }

        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string ProfileImage { get; set; }

        //[Display(Name = "BranchName", ResourceType = typeof(Wording))]
        //public string BranchName { get; set; }

        [Display(Name = "GenderName", ResourceType = typeof(Wording))]
        public string GenderName { get; set; }

        //[Display(Name = "CodeBranch", ResourceType = typeof(Wording))]
        //public string BranchCode { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string CardIssuedName { get; set; }

        [Display(Name = "CountryId", ResourceType = typeof(Wording))]
        public string CountryId { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ProvinceName { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string DistrictName { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string WardName { get; set; }

        [Display(Name = "Username", ResourceType = typeof(Wording))]
        public Nullable<int> UserId { get; set; }
        //[Display(Name = "CodeDepartment", ResourceType = typeof(Wording))]
        //public string CodeDepartment { get; set; }
        /// <summary>
        /// tạo user đăng nhập cho nhân viên
        /// </summary>
        //[Required(ErrorMessageResourceName = "Username_Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Username", ResourceType = typeof(Wording))]
        public string UserName { get; set; }

        //[Required(ErrorMessageResourceName = "Password_Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceName = "Password_LengthError", ErrorMessageResourceType = typeof(Error), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Wording))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "ConfirmPassword_NotMatch", ErrorMessageResourceType = typeof(Error))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public UserStatus? Status { get; set; }

        [Display(Name = "StartDateWork", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(Name = "EndDateWork", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Display(Name = "Email2", ResourceType = typeof(Wording))]
        public string Email2 { get; set; }

        [Display(Name = "Phone2", ResourceType = typeof(Wording))]
        public string Phone2 { get; set; }
        public int CountFamily { get; set; }
        //[Display(Name = "CheckInOut_UserId", ResourceType = typeof(Wording))]
        //public int? CheckInOut_UserId { get; set; }
        [Display(Name = "IsWorking", ResourceType = typeof(Wording))]
        public Nullable<bool> IsWorking { get; set; }
        [Display(Name = "DrugStore", ResourceType = typeof(Wording))]
        public string DrugStore { get; set; }
              [Display(Name = "UserManager", ResourceType = typeof(Wording))]
        public Nullable<int> StaffParentId { get; set; }
        [Display(Name = "PositionName", ResourceType = typeof(Wording))]
        public string PositionName { get; set; }

        [Display(Name = "PositionCode", ResourceType = typeof(Wording))]
        public string PositionCode { get; set; }
        [Display(Name = "CommissionPercent", ResourceType = typeof(Wording))]
        public decimal? CommissionPercent { get; set; }
        [Display(Name = "MinimumRevenue", ResourceType = typeof(Wording))]
        public decimal? MinimumRevenue { get; set; }
        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public Nullable<int> PositionId { get; set; }
        [Display(Name = "UserManager", ResourceType = typeof(Wording))]
        public string StaffParentName { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get; set; }
        public IEnumerable<SelectListItem> LiteracyList { get; set; }
        public IEnumerable<SelectListItem> MaritalStatusList { get; set; }
        public IEnumerable<SelectListItem> PositionList { get; set; }
        public IEnumerable<SelectListItem> ReligionList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> EthnicList { get; set; }

        public IEnumerable<SelectListItem> DistrictList { get; set; }
        public IEnumerable<SelectListItem> WardList { get; set; }
        public IEnumerable<SelectListItem> ProvinceList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }

        public List<StaffFamily> ListStaffFamily { get; set; }

        public IEnumerable<StaffFamilyViewModel> StaffFamilyList { get; set; }
        public IEnumerable<BankViewModel> BankList { get; set; }

        public List<vwFingerPrint> ListFingerPrint { get; set; }
    }
}