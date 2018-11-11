using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Sale.Models;

namespace Erp.BackOffice.Staff.Models
{
    public class LabourContractViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }

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
        [Display(Name = "ContractName", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "SignedDay", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> SignedDay { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ContactType", ResourceType = typeof(Wording))]
        public Nullable<int> Type { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "DayEffective", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> EffectiveDate { get; set; }

        [Display(Name = "ExpiryDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        public string Status2 { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ContentContractTerms", ResourceType = typeof(Wording))]
        public string Content { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ApprovedName", ResourceType = typeof(Wording))]
        public Nullable<int> ApprovedUserId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "WageAgreement", ResourceType = typeof(Wording))]
        public Nullable<int> WageAgreement { get; set; }
       [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "FormWork", ResourceType = typeof(Wording))]
        public string FormWork { get; set; }
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ContentJob", ResourceType = typeof(Wording))]
        public string Job { get; set; }

        [Display(Name = "ContractCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string PositionStaff { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string PositionApproved { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> DepartmentStaffId { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> DepartmentApprovedId { get; set; }

        //view
        //thông tin nhân viên.
        [Display(Name = "StaffName", ResourceType = typeof(Wording))]
        public string StaffName { get; set; }

        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string StaffProfileImage { get; set; }

        [Display(Name = "StaffCode", ResourceType = typeof(Wording))]
        public string StaffCode { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string StaffPositionId { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string StaffPositionName { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public string StaffDepartmentName { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string StaffBranchName { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string StaffPhone { get; set; }

        [Display(Name = "IdCardNumber", ResourceType = typeof(Wording))]
        public string StaffIdCardNumber { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string StaffCardIssuedName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string StaffAddress { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string StaffWard { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string StaffDistrict { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string StaffProvince { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StaffBirthday { get; set; }

        [Display(Name = "IdCardDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> StaffIdCardDate { get; set; }

        //thông tin người đại diện công ty
        [Display(Name = "FullName", ResourceType = typeof(Wording))]
        public string ApprovedUserName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Wording))]
        public string ApprovedUserCode { get; set; }

        [Display(Name = "ProfileImage", ResourceType = typeof(Wording))]
        public string ApprovedProfileImage { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string ApprovedUserPositionId { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string ApprovedUserPositionName { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string ApprovedBranchName { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public string ApprovedDepartmentName { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> StaffbranchId { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> ApprovedBranchId { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Wording))]
        public string ApprovedPhone { get; set; }

        [Display(Name = "IdCardNumber", ResourceType = typeof(Wording))]
        public string ApprovedIdCardNumber { get; set; }

        [Display(Name = "IdCardIssued", ResourceType = typeof(Wording))]
        public string ApprovedCardIssuedName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string ApprovedAddress { get; set; }

        [Display(Name = "Ward", ResourceType = typeof(Wording))]
        public string ApprovedWard { get; set; }

        [Display(Name = "District", ResourceType = typeof(Wording))]
        public string ApprovedDistrict { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Wording))]
        public string ApprovedProvince { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ApprovedBirthday { get; set; }

        [Display(Name = "IdCardDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ApprovedIdCardDate { get; set; }
        //view loại hợp đồng
        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public string CreatedUserName { get; set; }
        [Display(Name = "ContractTypeName", ResourceType = typeof(Wording))]
        public string ContractTypeName { get; set; }
        [Display(Name = "QuantityMonth", ResourceType = typeof(Wording))]
        public Nullable<int> QuantityMonth { get; set; }
        [Display(Name = "Notice", ResourceType = typeof(Wording))]
        public Nullable<int> Notice { get; set; }

        public IEnumerable<SelectListItem> StaffList { get; set; }
        public IEnumerable<SelectListItem> ApprovedUserList { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public IEnumerable<SelectListItem> FormWorkList { get; set; }
        public List<ObjectAttributeValueViewModel> AttributeValueList { get; set; }

    }
}