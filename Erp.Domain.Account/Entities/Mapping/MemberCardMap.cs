using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class MemberCardMap : EntityTypeConfiguration<MemberCard>
    {
        public MemberCardMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
        //    this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Status).HasMaxLength(50);
            this.Property(t => t.Code).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Account_MemberCard");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.DateOfIssue).HasColumnName("DateOfIssue");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.BranchId).HasColumnName("BranchId");

        }
    }
}
