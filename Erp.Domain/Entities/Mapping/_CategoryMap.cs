using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(150);
            this.Property(t => t.Value)
                .HasMaxLength(150);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(5000);            

            // Table & Column Mappings
            this.ToTable("System_Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");

            // Relationships
            this.HasOptional(t => t.ParentCategory)
                .WithMany(t => t.Categories)
                .HasForeignKey(d => d.ParentId);

        }
    }
}
