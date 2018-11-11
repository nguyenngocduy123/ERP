using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class TimekeepingSynthesisViewModel
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

        [Display(Name = "NgayCongThucTe", ResourceType = typeof(Wording))]
        public Nullable<int> NgayCongThucTe { get; set; }
        [Display(Name = "GioThucTe", ResourceType = typeof(Wording))]
        public Nullable<int> GioThucTe { get; set; }
        [Display(Name = "TangCaNgayThuong", ResourceType = typeof(Wording))]
        public Nullable<double> TangCaNgayThuong { get; set; }
        [Display(Name = "TangCaNgayNghi", ResourceType = typeof(Wording))]
        public Nullable<double> TangCaNgayNghi { get; set; }
        [Display(Name = "TangCaNgayLe", ResourceType = typeof(Wording))]
        public Nullable<double> TangCaNgayLe { get; set; }
        [Display(Name = "GioDiTre", ResourceType = typeof(Wording))]
        public Nullable<double> GioDiTre { get; set; }
        [Display(Name = "GioVeSom", ResourceType = typeof(Wording))]
        public Nullable<double> GioVeSom { get; set; }
        [Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }
        [Display(Name = "TongNgayNghi", ResourceType = typeof(Wording))]
        public Nullable<int> TongNgayNghi { get; set; }
        [Display(Name = "NgayNghiCoPhep", ResourceType = typeof(Wording))]
        public Nullable<int> NgayNghiCoPhep { get; set; }
        [Display(Name = "NgayNghiKoPhep", ResourceType = typeof(Wording))]
        public Nullable<int> NgayNghiKoPhep { get; set; }

        [Display(Name = "SoNgayNghiBu", ResourceType = typeof(Wording))]
        public Nullable<int> SoNgayNghiBu { get; set; }
        [Display(Name = "SoNgayNghiLe", ResourceType = typeof(Wording))]
        public Nullable<int> SoNgayNghiLe { get; set; }
        [Display(Name = "TrongGioNgayThuong", ResourceType = typeof(Wording))]
        public Nullable<double> TrongGioNgayThuong { get; set; }
        [Display(Name = "TrongGioNgayNghi", ResourceType = typeof(Wording))]
        public Nullable<double> TrongGioNgayNghi { get; set; }
        [Display(Name = "TrongGioNgayLe", ResourceType = typeof(Wording))]
        public Nullable<double> TrongGioNgayLe { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }
        [Display(Name = "NgayDiTre", ResourceType = typeof(Wording))]
        public Nullable<int> NgayDiTre { get; set; }
        [Display(Name = "NgayVeSom", ResourceType = typeof(Wording))]
        public Nullable<int> NgayVeSom { get; set; }
    }
}