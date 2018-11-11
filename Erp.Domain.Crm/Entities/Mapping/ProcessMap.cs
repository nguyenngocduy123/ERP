using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class ProcessMap : EntityTypeConfiguration<Process>
    {
        public ProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Category).HasMaxLength(150);
            this.Property(t => t.DataSource).HasMaxLength(100);
            this.Property(t => t.ActivateAs).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Crm_Process");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.DataSource).HasColumnName("DataSource");
            //this.Property(t => t.RecordIsCreated).HasColumnName("RecordIsCreated");
            //this.Property(t => t.RecordIsAssigned).HasColumnName("RecordIsAssigned");
            //this.Property(t => t.RecordFieldsChanges).HasColumnName("RecordFieldsChanges");
            //this.Property(t => t.RecordIsDeleted).HasColumnName("RecordIsDeleted");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ActivateAs).HasColumnName("ActivateAs");
            this.Property(t => t.QueryRecivedUser).HasColumnName("QueryRecivedUser");
        }
    }
}
