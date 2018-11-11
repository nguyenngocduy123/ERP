using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Erp.BackOffice.Areas.Administration.Models
{
    public class CreatePageModel
    {        
        public bool IsTranslate { get; set; }
        public PageModel Page { get; set; }

    }
}