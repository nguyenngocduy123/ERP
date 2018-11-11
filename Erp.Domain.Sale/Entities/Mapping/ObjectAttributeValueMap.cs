using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class ObjectAttributeValueMap : EntityTypeConfiguration<ObjectAttributeValue>
    {
        public ObjectAttributeValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value).HasMaxLength(200);


            // Table & Column Mappings
            this.ToTable("Sale_ObjectAttributeValue");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.ObjectId).HasColumnName("ObjectId");
            this.Property(t => t.AttributeId).HasColumnName("AttributeId");
            this.Property(t => t.Value).HasColumnName("Value");

        }
    }
}
