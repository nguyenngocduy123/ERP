using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwSalaryAdvanceMap : EntityTypeConfiguration<vwSalaryAdvance>
    {
        public vwSalaryAdvanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CodeAdvance).HasMaxLength(150);
                        this.Property(t => t.Status).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("vwStaff_SalaryAdvance");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.CodeAdvance).HasColumnName("CodeAdvance");

            this.Property(t => t.Pay).HasColumnName("Pay");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CodeStaff).HasColumnName("CodeStaff");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");

            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.Staff_DepartmentId).HasColumnName("Staff_DepartmentId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.DayAdvance).HasColumnName("DayAdvance");
        }
    }
}
