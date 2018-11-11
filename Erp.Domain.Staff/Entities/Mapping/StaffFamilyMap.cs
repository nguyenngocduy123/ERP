using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class StaffFamilyMap : EntityTypeConfiguration<StaffFamily>
    {
        public StaffFamilyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Address).HasMaxLength(250);
            this.Property(t => t.Correlative).HasMaxLength(10);
            this.Property(t => t.Phone).HasMaxLength(11);


            // Table & Column Mappings
            this.ToTable("Staff_StaffFamily");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Correlative).HasColumnName("Correlative");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.IsDependencies).HasColumnName("IsDependencies");

        }
    }
}
