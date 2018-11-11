using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class ShiftsMap : EntityTypeConfiguration<Shifts>
    {
        public ShiftsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Code).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Staff_Shifts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.StartTimeOut).HasColumnName("StartTimeOut");
            this.Property(t => t.EndTimeIn).HasColumnName("EndTimeIn");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.NightShifts).HasColumnName("NightShifts");
            this.Property(t => t.CategoryShifts).HasColumnName("CategoryShifts");
            this.Property(t => t.StartTimeIn).HasColumnName("StartTimeIn");
            this.Property(t => t.EndTimeOut).HasColumnName("EndTimeOut");
            this.Property(t => t.MinuteEarly).HasColumnName("MinuteEarly");
            this.Property(t => t.MinuteLate).HasColumnName("MinuteLate");
        }
    }
}
