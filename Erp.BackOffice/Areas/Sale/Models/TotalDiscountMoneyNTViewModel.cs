using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.Domain.Sale.Entities;

namespace Erp.BackOffice.Sale.Models
{
    public class TotalDiscountMoneyNTViewModel
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


        [Display(Name = "DrugStoreId", ResourceType = typeof(Wording))]
        public Nullable<int> DrugStoreId { get; set; }
        [Display(Name = "BranchId", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }
        [Display(Name = "UserManagerId", ResourceType = typeof(Wording))]
        public Nullable<int> UserManagerId { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }
        [Display(Name = "QuantityDay", ResourceType = typeof(Wording))]
        public Nullable<int> QuantityDay { get; set; }
        [Display(Name = "PercentDecrease", ResourceType = typeof(Wording))]
        public decimal? PercentDecrease { get; set; }
        [Display(Name = "DisCountAmount", ResourceType = typeof(Wording))]
        public decimal? DiscountAmount { get; set; }
        [Display(Name = "DecreaseAmount", ResourceType = typeof(Wording))]
        public decimal? DecreaseAmount { get; set; }
        [Display(Name = "RemainingAmount", ResourceType = typeof(Wording))]
        public decimal? RemainingAmount { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public string Status { get; set; }
        // vw
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public string BranchName { get; set; }
        [Display(Name = "CodeBranch", ResourceType = typeof(Wording))]
        public string BranchCode { get; set; }
        [Display(Name = "DrugStoreName", ResourceType = typeof(Wording))]
        public string DrugStoreName { get; set; }
        [Display(Name = "DrugStoreCode", ResourceType = typeof(Wording))]
        public string DrugStoreCode { get; set; }
        public string UserManagerName { get; set; }
        public string UserManagerCode { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public string WardName { get; set; }
        public decimal? DoanhSo { get; set; }
        public decimal? ChietKhauCoDinh { get; set; }
        public decimal? ChietKhauDotXuat { get; set; }
        public int? PointVIP { get; set; }
        public List<vwProductInvoiceDetail> DetailList { get; set; }
        public List<Sale_BaoCaoNhapXuatTonViewModel> ListNXT { get; set; }
    }
}