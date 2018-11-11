using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Crm.Models
{
    public class ProcessApplyViewModel
    {
        public string ProcessEntity { get; set; }
        public object EntityData { get; set; }
        public List<ProcessStageViewModel> ListProcessStage { get; set; }
        public List<Administration.Models.EditViewField> ListEditViewField { get; set; }
    }
}