using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwWelfareProgramsDetailMap : EntityTypeConfiguration<vwWelfareProgramsDetail>
    {
        public vwWelfareProgramsDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note).HasMaxLength(250);


            // Table & Column Mappings
            this.ToTable("vwStaff_WelfareProgramsDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.WelfareProgramsId).HasColumnName("WelfareProgramsId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.PositionName).HasColumnName("PositionName");
        }
    }
}
