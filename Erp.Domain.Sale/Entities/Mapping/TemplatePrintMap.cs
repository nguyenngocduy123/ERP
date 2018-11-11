using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class TemplatePrintMap : EntityTypeConfiguration<TemplatePrint>
    {
        public TemplatePrintMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title).HasMaxLength(250);
            this.Property(t => t.Code).HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("System_TemplatePrint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ContentDefault).HasColumnName("ContentDefault");
        }
    }
}
