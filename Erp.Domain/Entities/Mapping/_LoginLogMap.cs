using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class LoginLogMap : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TypeWebsite)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("System_LoginLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LoginTime).HasColumnName("LoginTime");
            this.Property(t => t.TypeWebsite).HasColumnName("TypeWebsite");
        }
    }
}
