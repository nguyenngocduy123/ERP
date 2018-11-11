using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class TechniqueMap : EntityTypeConfiguration<Technique>
    {
        public TechniqueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.IdCardIssued).HasMaxLength(50);
            this.Property(t => t.Rank).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Staff_Technique");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.IdCardIssued).HasColumnName("IdCardIssued");
            this.Property(t => t.IdCardDate).HasColumnName("IdCardDate");
            this.Property(t => t.Rank).HasColumnName("Rank");
            this.Property(t => t.StaffId).HasColumnName("StaffId");

        }
    }
}
