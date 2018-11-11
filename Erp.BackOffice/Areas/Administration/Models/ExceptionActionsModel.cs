using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    [Serializable]
    public class ExceptionActionsModel
    {
        public List<ExceptionActionModel> ExceptionActions  { get; set; }
    }
}