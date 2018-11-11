using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwBranchDepartmentMap : EntityTypeConfiguration<vwBranchDepartment>
    {
        public vwBranchDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            

            // Table & Column Mappings
            this.ToTable("vwStaff_BranchDepartment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Staff_DepartmentId).HasColumnName("Staff_DepartmentId");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.BranchCode).HasColumnName("BranchCode");
            this.Property(t => t.CodeDepartment).HasColumnName("CodeDepartment");
            this.Property(t => t.MaxDebitAmount).HasColumnName("MaxDebitAmount");
        }
    }
}
