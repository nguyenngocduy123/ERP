using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwAccount_LiabilitiesMap : EntityTypeConfiguration<vwAccount_Liabilities>
    {
        public vwAccount_LiabilitiesMap()
        {
            // Primary Key
            this.HasKey(t => t.TargetCode);

            // Properties
            this.Property(t => t.TargetName).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("vwAccount_Liabilities");
            this.Property(t => t.TargetModule).HasColumnName("TargetModule");
            this.Property(t => t.TargetCode).HasColumnName("TargetCode");
            this.Property(t => t.TargetName).HasColumnName("TargetName");
            this.Property(t => t.Remain).HasColumnName("Remain");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
        }
    }
}
