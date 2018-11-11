using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class SalaryTableMap : EntityTypeConfiguration<SalaryTable>
    {
        public SalaryTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.Status).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("Staff_SalaryTable");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.TargetMonth).HasColumnName("TargetMonth");
            this.Property(t => t.TargetYear).HasColumnName("TargetYear");
            this.Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            this.Property(t => t.SalarySettingId).HasColumnName("SalarySettingId");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ListSalarySettingDetail).HasColumnName("ListSalarySettingDetail");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.IsSend).HasColumnName("IsSend");
            this.Property(t => t.Submitted).HasColumnName("Submitted");
            this.Property(t => t.SalaryApprovalType).HasColumnName("SalaryApprovalType");
            this.Property(t => t.TotalSalary).HasColumnName("TotalSalary");
            this.Property(t => t.HiddenForMonth).HasColumnName("HiddenForMonth");
        }
    }
}
