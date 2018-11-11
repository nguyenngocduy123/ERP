using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    public class VoteQuestionViewModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public List<VoteAnswerViewModel> ListVoteAnswer { get; set; }
    }

    public class VoteAnswerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public int NumberOfVote { get; set; }
        public double PercentOfVote { get; set; }
    }
}