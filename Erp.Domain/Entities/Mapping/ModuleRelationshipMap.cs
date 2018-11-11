using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class ModuleRelationshipMap : EntityTypeConfiguration<ModuleRelationship>
    {
        public ModuleRelationshipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.First_MetadataFieldName).HasMaxLength(100);
            this.Property(t => t.Second_ModuleName).HasMaxLength(100);
            this.Property(t => t.Second_MetadataFieldName).HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("System_ModuleRelationship");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.First_ModuleName).HasColumnName("First_ModuleName");
            this.Property(t => t.First_MetadataFieldName).HasColumnName("First_MetadataFieldName");
            this.Property(t => t.Second_ModuleName).HasColumnName("Second_ModuleName");
            this.Property(t => t.Second_ModuleName_Alias).HasColumnName("Second_ModuleName_Alias");
            this.Property(t => t.Second_MetadataFieldName).HasColumnName("Second_MetadataFieldName");

        }
    }
}
