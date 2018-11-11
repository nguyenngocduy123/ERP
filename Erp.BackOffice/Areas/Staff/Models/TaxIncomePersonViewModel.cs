using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class TaxIncomePersonViewModel
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

        
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "NameOfThePayingAgency", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "TaxCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "GeneralTaxation", ResourceType = typeof(Wording))]
        public string GeneralTaxationId { get; set; }

        [Required(ErrorMessageResourceName = "RequireItemSelection", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "GeneralManage", ResourceType = typeof(Wording))]
        public string GeneralManageId { get; set; }
        public DateTime? StaffStartDate { get; set; }
        public DateTime? StaffEndDate { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public int? BranchId { get; set; }
        public IEnumerable<StaffsTaxModel> ListStaffsTax { get; set; }
        //public decimal TongThuNhapChiuThue { get; internal set; }
        //public decimal TongGiamTru { get; internal set; }
        //public decimal ThuNhapTinhThue { get; internal set; }
        //public decimal ThueTamTinh { get; internal set; }
    }
}