using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class CampaignMap : EntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Type).HasMaxLength(150);
            this.Property(t => t.Status).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("Crm_Campaign");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.BudgetedCost).HasColumnName("BudgetedCost");
            this.Property(t => t.ExpectedRevenue).HasColumnName("ExpectedRevenue");
            this.Property(t => t.ActualCost).HasColumnName("ActualCost");
            this.Property(t => t.ExpectedResponse).HasColumnName("ExpectedResponse");
            this.Property(t => t.Commision).HasColumnName("Commision");

        }
    }
}
