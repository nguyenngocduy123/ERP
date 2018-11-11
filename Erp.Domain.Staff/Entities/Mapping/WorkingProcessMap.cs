using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class WorkingProcessMap : EntityTypeConfiguration<WorkingProcess>
    {
        public WorkingProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.WorkPlace).HasMaxLength(150);
            this.Property(t => t.Position).HasMaxLength(50);
            this.Property(t => t.Email).HasMaxLength(50);
            this.Property(t => t.Phone).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Staff_WorkingProcess");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.WorkPlace).HasColumnName("WorkPlace");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.DayStart).HasColumnName("DayStart");
            this.Property(t => t.DayEnd).HasColumnName("DayEnd");
            this.Property(t => t.BonusDisciplineId).HasColumnName("BonusDisciplineId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
        }
    }
}
