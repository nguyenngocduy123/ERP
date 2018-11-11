using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwServiceReminderGroupMap : EntityTypeConfiguration<vwServiceReminderGroup>
    {
        public vwServiceReminderGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            

            // Table & Column Mappings
            this.ToTable("vwSale_ServiceReminderGroup");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");


            this.Property(t => t.ServiceId).HasColumnName("ServiceId");
            this.Property(t => t.ServiceReminderId).HasColumnName("ServiceReminderId");

            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Reminder).HasColumnName("Reminder");
            this.Property(t => t.QuantityDate).HasColumnName("QuantityDate");

        }
    }
}
