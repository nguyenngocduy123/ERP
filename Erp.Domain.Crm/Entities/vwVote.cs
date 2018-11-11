using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities
{
    public class vwVote
    {
        public vwVote()
        {
            
        }

        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public int NumberOfVote { get; set; }
    }
}
