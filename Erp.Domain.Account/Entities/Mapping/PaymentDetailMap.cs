using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class PaymentDetailMap : EntityTypeConfiguration<PaymentDetail>
    {
        public PaymentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.MaChungTuGoc).HasMaxLength(20);
            this.Property(t => t.LoaiChungTuGoc).HasMaxLength(20);


            // Table & Column Mappings
            this.ToTable("Account_PaymentDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.PaymentId).HasColumnName("PaymentId");
            this.Property(t => t.MaChungTuGoc).HasColumnName("MaChungTuGoc");
            this.Property(t => t.LoaiChungTuGoc).HasColumnName("LoaiChungTuGoc");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
        }
    }
}
