using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class SymbolTimekeepingMap : EntityTypeConfiguration<SymbolTimekeeping>
    {
        public SymbolTimekeepingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            

            // Table & Column Mappings
            this.ToTable("Staff_SymbolTimekeeping");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Timekeeping).HasColumnName("Timekeeping");
            this.Property(t => t.DayOff).HasColumnName("DayOff");
            this.Property(t => t.Color).HasColumnName("Color");
            this.Property(t => t.CodeDefault).HasColumnName("CodeDefault");
            this.Property(t => t.Absent).HasColumnName("Absent");
            this.Property(t => t.Icon).HasColumnName("Icon");
        }
    }
}
