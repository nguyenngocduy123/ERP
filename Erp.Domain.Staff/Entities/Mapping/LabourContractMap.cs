using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class LabourContractMap : EntityTypeConfiguration<LabourContract>
    {
        public LabourContractMap()
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
            this.ToTable("Staff_LabourContract");
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

        }
    }
}
