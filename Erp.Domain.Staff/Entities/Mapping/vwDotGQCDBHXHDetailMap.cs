using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwDotGQCDBHXHDetailMap : EntityTypeConfiguration<vwDotGQCDBHXHDetail>
    {
        public vwDotGQCDBHXHDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.SocietyCode).HasMaxLength(100);
            this.Property(t => t.DKTH_TinhTrang).HasMaxLength(250);
            this.Property(t => t.PaymentMethod).HasMaxLength(250);
            this.Property(t => t.Note).HasMaxLength(250);


            // Table & Column Mappings
            this.ToTable("vwStaff_DotGQCDBHXHDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.DotGQCDBHXHId).HasColumnName("DotGQCDBHXHId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.DayOffId).HasColumnName("DayOffId");
            this.Property(t => t.SocietyCode).HasColumnName("SocietyCode");
            this.Property(t => t.DKTH_TinhTrang).HasColumnName("DKTH_TinhTrang");
            this.Property(t => t.DKTH_ThoiDiem).HasColumnName("DKTH_ThoiDiem");
            this.Property(t => t.DayStart).HasColumnName("DayStart");
            this.Property(t => t.DayEnd).HasColumnName("DayEnd");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.DayOffName).HasColumnName("DayOffName");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}
