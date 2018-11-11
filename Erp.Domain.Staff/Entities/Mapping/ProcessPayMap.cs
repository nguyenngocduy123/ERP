using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class ProcessPayMap : EntityTypeConfiguration<ProcessPay>
    {
        public ProcessPayMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
                        this.Property(t => t.CodePay).HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Staff_ProcessPay");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.DayDecision).HasColumnName("DayDecision");
            this.Property(t => t.DayEffective).HasColumnName("DayEffective");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.CodePay).HasColumnName("CodePay");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.BasicPay).HasColumnName("BasicPay");
        }
    }
}
