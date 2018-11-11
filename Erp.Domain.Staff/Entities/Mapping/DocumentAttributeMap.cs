using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class DocumentAttributeMap : EntityTypeConfiguration<DocumentAttribute>
    {
        public DocumentAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.File).HasMaxLength(250);
            this.Property(t => t.Note).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("Staff_DocumentAttribute");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.DocumentFieldId).HasColumnName("DocumentFieldId");
            this.Property(t => t.File).HasColumnName("File");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.TypeFile).HasColumnName("TypeFile");
        }
    }
}
