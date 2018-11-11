using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwBonusDisciplineMap : EntityTypeConfiguration<vwBonusDiscipline>
    {
        public vwBonusDisciplineMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Category).HasMaxLength(150);
            this.Property(t => t.Formality).HasMaxLength(150);
            this.Property(t => t.Reason).HasMaxLength(350);
            this.Property(t => t.Note).HasMaxLength(350);


            // Table & Column Mappings
            this.ToTable("vwStaff_BonusDiscipline");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Formality).HasColumnName("Formality");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.DayDecision).HasColumnName("DayDecision");
            this.Property(t => t.DayEffective).HasColumnName("DayEffective");
            this.Property(t => t.PlaceDecisions).HasColumnName("PlaceDecisions");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Code).HasColumnName("Code");

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CodeName).HasColumnName("CodeName");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.Staff_DepartmentId).HasColumnName("Staff_DepartmentId");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.Position).HasColumnName("Position");

            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.CreatedUserName).HasColumnName("CreatedUserName");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.PlaceDecisionsName).HasColumnName("PlaceDecisionsName");
            this.Property(t => t.PlaceDecisions_Branch).HasColumnName("PlaceDecisions_Branch");
            this.Property(t => t.Money).HasColumnName("Money");
        }
    }
}
