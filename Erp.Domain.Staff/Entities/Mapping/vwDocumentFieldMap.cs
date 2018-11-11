using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwDocumentFieldMap : EntityTypeConfiguration<vwDocumentField>
    {
        public vwDocumentFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.Code).HasMaxLength(50);
            this.Property(t => t.IsSearch).HasMaxLength(50);
            this.Property(t => t.Category).HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("vwStaff_DocumentField");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.DocumentTypeId).HasColumnName("DocumentTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsSearch).HasColumnName("IsSearch");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
        }
    }
}
