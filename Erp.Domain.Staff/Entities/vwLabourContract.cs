using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwLabourContract
    {
        public vwLabourContract()
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

        public Nullable<System.DateTime> SignedDay { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public Nullable<int> ApprovedUserId { get; set; }
        public Nullable<int> WageAgreement { get; set; }
        public string FormWork { get; set; }
        public string Job { get; set; }
        public string Code { get; set; }
        public string PositionStaff { get; set; }
        public string PositionApproved { get; set; }
        public Nullable<int> DepartmentStaffId { get; set; }
        public Nullable<int> DepartmentApprovedId { get; set; }

        //view nhân viên
        public string StaffName { get; set; }
        public string StaffProfileImage { get; set; }
        public string StaffCode { get; set; }
        public string StaffPositionId { get; set; }
        public string StaffPositionName { get; set; }
        public string StaffDepartmentName { get; set; }
        public string StaffBranchName { get; set; }
        public int? StaffbranchId { get; set; }

        public Nullable<System.DateTime> StaffBirthday { get; set; }
        public string StaffPhone { get; set; }
        public string StaffIdCardNumber { get; set; }
        public Nullable<System.DateTime> StaffIdCardDate { get; set; }
        public string StaffCardIssuedName { get; set; }
        public string StaffAddress { get; set; }
        public string StaffWard { get; set; }
        public string StaffDistrict { get; set; }
        public string StaffProvince { get; set; }
        //view người đại diện công ty
        public string ApprovedUserName { get; set; }
        public string ApprovedUserCode { get; set; }
        public string ApprovedProfileImage { get; set; }
        public string ApprovedUserPositionId { get; set; }
        public string ApprovedUserPositionName { get; set; }
        public int? ApprovedBranchId { get; set; }
        public string ApprovedDepartmentName { get; set; }
        public string ApprovedBranchName { get; set; }

        public Nullable<System.DateTime> ApprovedBirthday { get; set; }
        public string ApprovedPhone { get; set; }
        public string ApprovedIdCardNumber { get; set; }
        public Nullable<System.DateTime> ApprovedIdCardDate { get; set; }
        public string ApprovedCardIssuedName { get; set; }
        public string ApprovedAddress { get; set; }
        public string ApprovedWard { get; set; }
        public string ApprovedDistrict { get; set; }
        public string ApprovedProvince { get; set; }
        //view loại hợp đồng
        public string CreatedUserName { get; set; }
        public string ContractTypeName { get; set; }
        public Nullable<int> QuantityMonth { get; set; }
        public Nullable<int> Notice { get; set; }
    }
}
