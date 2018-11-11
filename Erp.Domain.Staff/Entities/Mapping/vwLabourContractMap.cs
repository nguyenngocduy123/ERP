using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwLabourContractMap : EntityTypeConfiguration<vwLabourContract>
    {
        public vwLabourContractMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.Status).HasMaxLength(50);
            //this.Property(t => t.Content).HasMaxLength(550);
            this.Property(t => t.FormWork).HasMaxLength(150);
            //this.Property(t => t.Job).HasMaxLength(150);
            this.Property(t => t.Code).HasMaxLength(50);
            this.Property(t => t.PositionStaff).HasMaxLength(150);
            this.Property(t => t.PositionApproved).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("vwStaff_LabourContract");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.SignedDay).HasColumnName("SignedDay");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.EffectiveDate).HasColumnName("EffectiveDate");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ApprovedUserId).HasColumnName("ApprovedUserId");
            this.Property(t => t.WageAgreement).HasColumnName("WageAgreement");
            this.Property(t => t.FormWork).HasColumnName("FormWork");
            this.Property(t => t.Job).HasColumnName("Job");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.PositionStaff).HasColumnName("PositionStaff");
            this.Property(t => t.PositionApproved).HasColumnName("PositionApproved");
            this.Property(t => t.DepartmentStaffId).HasColumnName("DepartmentStaffId");
            this.Property(t => t.DepartmentApprovedId).HasColumnName("DepartmentApprovedId");
            //view nhân viên
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.StaffProfileImage).HasColumnName("StaffProfileImage");
            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.StaffPositionId).HasColumnName("StaffPositionId");
            this.Property(t => t.StaffDepartmentName).HasColumnName("StaffDepartmentName");
            this.Property(t => t.StaffBranchName).HasColumnName("StaffBranchName");
            this.Property(t => t.StaffbranchId).HasColumnName("StaffbranchId");
            //view người đại diện công ty
            this.Property(t => t.StaffPositionName).HasColumnName("StaffPositionName");
            this.Property(t => t.ApprovedUserName).HasColumnName("ApprovedUserName");
            this.Property(t => t.ApprovedUserCode).HasColumnName("ApprovedUserCode");
            this.Property(t => t.ApprovedProfileImage).HasColumnName("ApprovedProfileImage");
            this.Property(t => t.ApprovedUserPositionId).HasColumnName("ApprovedUserPositionId");
            this.Property(t => t.ApprovedUserPositionName).HasColumnName("ApprovedUserPositionName");
            this.Property(t => t.ApprovedBranchId).HasColumnName("ApprovedBranchId");
            this.Property(t => t.ApprovedDepartmentName).HasColumnName("ApprovedDepartmentName");
            this.Property(t => t.ApprovedBranchName).HasColumnName("ApprovedBranchName");
            //view loại hợp đồng
            this.Property(t => t.CreatedUserName).HasColumnName("CreatedUserName");
            this.Property(t => t.ContractTypeName).HasColumnName("ContractTypeName");
            this.Property(t => t.QuantityMonth).HasColumnName("QuantityMonth");
            this.Property(t => t.Notice).HasColumnName("Notice");

        }
    }
}
