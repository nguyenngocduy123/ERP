using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class vwVoteMap : EntityTypeConfiguration<vwVote>
    {
        public vwVoteMap()
        {
            // Primary Key
            this.HasKey(t => new { t.QuestionId, t.AnswerId });

            // Properties

            // Table & Column Mappings
            this.ToTable("vwCrm_Vote");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.QuestionName).HasColumnName("QuestionName");
            this.Property(t => t.AnswerId).HasColumnName("AnswerId");
            this.Property(t => t.AnswerName).HasColumnName("AnswerName");
            this.Property(t => t.NumberOfVote).HasColumnName("NumberOfVote");

        }
    }
}
