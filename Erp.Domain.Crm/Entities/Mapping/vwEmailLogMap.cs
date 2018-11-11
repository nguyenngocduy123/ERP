using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class vwEmailLogMap : EntityTypeConfiguration<vwEmailLog>
    {
        public vwEmailLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email).HasMaxLength(100);
            this.Property(t => t.Status).HasMaxLength(150);
            this.Property(t => t.SubjectEmail).HasMaxLength(150);
            this.Property(t => t.TargetModule).HasMaxLength(150);
            this.Property(t => t.Customer).HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("vwCrm_EmailLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");

            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.TargetModule).HasColumnName("TargetModule");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.SubjectEmail).HasColumnName("SubjectEmail");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.SentDate).HasColumnName("SentDate");
            
            this.Property(t => t.Customer).HasColumnName("Customer");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
