using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Erp.Domain.Entities.Mapping
{
    public class BOLogMap : EntityTypeConfiguration<BOLog>
    {
        public BOLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Action)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Note)
                .IsRequired();

            this.Property(t => t.Controller)
                .HasMaxLength(200);

            this.Property(t => t.Area)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("System_BOLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Controller).HasColumnName("Controller");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}
