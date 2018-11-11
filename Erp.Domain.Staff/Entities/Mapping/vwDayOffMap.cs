using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwDayOffMap : EntityTypeConfiguration<vwDayOff>
    {
        public vwDayOffMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            

            // Table & Column Mappings
            this.ToTable("vwStaff_DayOff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.NameSymbol).HasColumnName("NameSymbol");
            this.Property(t => t.CodeSymbol).HasColumnName("CodeSymbol");
            this.Property(t => t.DayStart).HasColumnName("DayStart");
            this.Property(t => t.DayEnd).HasColumnName("DayEnd");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.TypeDayOffId).HasColumnName("TypeDayOffId");
            this.Property(t => t.QuantityNotUsed).HasColumnName("QuantityNotUsed");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.TypeDayOffQuantity).HasColumnName("TypeDayOffQuantity");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.NameStaff).HasColumnName("NameStaff");
            this.Property(t => t.CodeStaff).HasColumnName("CodeStaff");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
        }
    }
}
