using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class UserSettingMap : EntityTypeConfiguration<UserSetting>
    {
        public UserSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("System_UserSetting");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SettingId).HasColumnName("SettingId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
