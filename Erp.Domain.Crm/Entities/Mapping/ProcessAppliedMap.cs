using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class ProcessAppliedMap : EntityTypeConfiguration<ProcessApplied>
    {
        public ProcessAppliedMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            //this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.ActionName).HasMaxLength(50);
            this.Property(t => t.ModuleName).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Crm_ProcessApplied");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            //this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.ProcessId).HasColumnName("ProcessId");

        }
    }
}
