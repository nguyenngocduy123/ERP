using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class vwTimekeepingSynthesisViewModel
    {
        public int Id { get; set; }

        [Display(Name = "StaffId", ResourceType = typeof(Wording))]
        public Nullable<int> StaffId { get; set; }
        [Display(Name = "Month", ResourceType = typeof(Wording))]
        public Nullable<int> Month { get; set; }
        [Display(Name = "Year", ResourceType = typeof(Wording))]
        public Nullable<int> Year { get; set; }
        [Display(Name = "NgayCongThucTe", ResourceType = typeof(Wording))]
        public Nullable<int> NgayCongThucTe { get; set; }
        [Display(Name = "NgayNghiCoPhep", ResourceType = typeof(Wording))]
        public Nullable<int> NgayNghiCoPhep { get; set; }
        [Display(Name = "SoNgayNghiBu", ResourceType = typeof(Wording))]
        public Nullable<int> SoNgayNghiBu { get; set; }
        [Display(Name = "SoNgayNghiLe", ResourceType = typeof(Wording))]
        public Nullable<int> SoNgayNghiLe { get; set; }

        [Display(Name = "TrongGioNgayThuong", ResourceType = typeof(Wording))]
        public Nullable<double> TrongGioNgayThuong { get; set; }
        [Display(Name = "TangCaNgayThuong", ResourceType = typeof(Wording))]
        public Nullable<double> TangCaNgayThuong { get; set; }
        [Display(Name = "TrongGioNgayNghi", ResourceType = typeof(Wording))]
        public Nullable<double> TrongGioNgayNghi { get; set; }
        [Display(Name = "TangCaNgayNghi", ResourceType = typeof(Wording))]
        public Nullable<double> TangCaNgayNghi { get; set; }
        [Display(Name = "TrongGioNgayLe", ResourceType = typeof(Wording))]
        public Nullable<double> TrongGioNgayLe { get; set; }
        [Display(Name = "TangCaNgayLe", ResourceType = typeof(Wording))]
        public Nullable<double> TangCaNgayLe { get; set; }
       
        [Display(Name = "GioDiTre", ResourceType = typeof(Wording))]
        public Nullable<double> GioDiTre { get; set; }
        [Display(Name = "GioVeSom", ResourceType = typeof(Wording))]
        public Nullable<double> GioVeSom { get; set; }
        [Display(Name = "GioLamCaDem", ResourceType = typeof(Wording))]
        public Nullable<double> GioLamCaDem { get; set; }

        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }
        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }
        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> Sale_BranchId { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Wording))]
        public Nullable<int> BranchDepartmentId { get; set; }

        [Display(Name = "TongNgayNghi", ResourceType = typeof(Wording))]
        public Nullable<int> TongNgayNghi { get; set; }
        [Display(Name = "NgayNghiKhongPhep", ResourceType = typeof(Wording))]
        public Nullable<int> NgayNghiKhongPhep { get; set; }
        [Display(Name = "TongGioTangCa", ResourceType = typeof(Wording))]
        public Nullable<double> TongGioTangCa { get; set; }
        [Display(Name = "TongGioLamTheoCa", ResourceType = typeof(Wording))]
        public Nullable<double> TongGioLamTheoCa { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }
        [Display(Name = "NgayDiTre", ResourceType = typeof(Wording))]
        public Nullable<int> NgayDiTre { get; set; }
        [Display(Name = "NgayVeSom", ResourceType = typeof(Wording))]
        public Nullable<int> NgayVeSom { get; set; }
    }
}