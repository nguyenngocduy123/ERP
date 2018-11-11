using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwDotBCBHXHDetail
    {
        public vwDotBCBHXHDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }
        public Nullable<int> DotBCBHXHId { get; set; }
        public Nullable<int> SocialInsuranceId { get; set; }
        public string Type { get; set; }

        public Nullable<int> StaffId { get; set; }
        public string MedicalCode { get; set; }
        public Nullable<System.DateTime> MedicalStartDate { get; set; }
        public Nullable<System.DateTime> MedicalEndDate { get; set; }
        public string MedicalIssue { get; set; }
        public decimal? MedicalDefaultValue { get; set; }
        public string SocietyCode { get; set; }
        public Nullable<System.DateTime> SocietyStartDate { get; set; }
        public Nullable<System.DateTime> SocietyEndDate { get; set; }
        public string SocietyIssue { get; set; }
        public decimal? SocietyDefaultValue { get; set; }
        public decimal? PC_CV { get; set; }
        public decimal? PC_TNVK { get; set; }
        public decimal? PC_TNN { get; set; }
        public decimal? PC_Khac { get; set; }
        public decimal? TienLuong { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string StaffName { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Gender { get; set; }
        public string IdCardNumber { get; set; }
        public DateTime? IdCardDate { get; set; }
        public string IdCardIssued { get; set; }
        public string BranchName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public string Address { get; set; }
        public string PositionName { get; set; }

    }
}
