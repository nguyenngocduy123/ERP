using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class DayOffMap : EntityTypeConfiguration<DayOff>
    {
        public DayOffMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            this.ToTable("Staff_DayOff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
     
            this.Property(t => t.DayStart).HasColumnName("DayStart");
            this.Property(t => t.DayEnd).HasColumnName("DayEnd");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.TypeDayOffId).HasColumnName("TypeDayOffId");
            this.Property(t => t.QuantityNotUsed).HasColumnName("QuantityNotUsed");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
