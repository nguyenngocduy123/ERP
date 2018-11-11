using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class TaxIncomePersonDetailViewModel
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

        [Required(ErrorMessageResourceName = "Staff", ErrorMessageResourceType = typeof(Error))]
        public int? StaffId { get; set; }

        [Display(Name = "TaxIncomePersonId", ResourceType = typeof(Wording))]
        public Nullable<int> TaxIncomePersonId { get; set; }
        
        public string Code { get; set; }

        public string Name { get; set; }

        public string PositionName { get; set; }

        public string BranchName { get; set; }

        public int? Sale_BranchId { get; set; }

        public bool? Gender { get; set; }

        public string GenderName { get; set; }

        public string Email { get; set; }

        public string IdCardNumber { get; set; }

        public string CountryId { get; set; }

        public string ProvinceName { get; set; }

        public string DistrictName { get; set; }

        public string WardName { get; set; }
        public decimal TongThuNhapChiuThue { get;  set; }
        public decimal TongGiamTru { get;  set; }
        public decimal ThuNhapTinhThue { get;  set; }
        public decimal ThueTamTinh { get;  set; }
    }
}