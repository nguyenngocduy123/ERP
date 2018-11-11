using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Status).HasMaxLength(50);
            this.Property(t => t.ParentType).HasMaxLength(50);
            this.Property(t => t.Description).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("Crm_Task");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.ParentType).HasColumnName("ParentType");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.ContactId).HasColumnName("ContactId");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsSendNotifications).HasColumnName("IsSendNotifications");
        }
    }
}
