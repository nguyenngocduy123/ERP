using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class TimekeepingMap : EntityTypeConfiguration<Timekeeping>
    {
        public TimekeepingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            

            // Table & Column Mappings
            this.ToTable("Staff_Timekeeping");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.HoursIn).HasColumnName("HoursIn");
            this.Property(t => t.HoursOut).HasColumnName("HoursOut");
            //this.Property(t => t.Pay).HasColumnName("Pay");
            this.Property(t => t.ShiftsId).HasColumnName("ShiftsId");
            //this.Property(t => t.Money).HasColumnName("Money");
          
        }
    }
}
