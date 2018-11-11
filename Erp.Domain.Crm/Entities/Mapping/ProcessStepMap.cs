using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class ProcessStepMap : EntityTypeConfiguration<ProcessStep>
    {
        public ProcessStepMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.StepValue).HasMaxLength(150);
            this.Property(t => t.EditControl).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("ProcessStep");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.StageId).HasColumnName("StageId");
            this.Property(t => t.StepValue).HasColumnName("StepValue");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");
            this.Property(t => t.IsSequential).HasColumnName("IsSequential");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.EditControl).HasColumnName("EditControl");

        }
    }
}
