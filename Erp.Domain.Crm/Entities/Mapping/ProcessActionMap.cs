using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class ProcessActionMap : EntityTypeConfiguration<ProcessAction>
    {
        public ProcessActionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.ActionType).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("Crm_ProcessAction");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.TemplateObject).HasColumnName("TemplateObject");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ProcessId).HasColumnName("ProcessId");
        }
    }
}
