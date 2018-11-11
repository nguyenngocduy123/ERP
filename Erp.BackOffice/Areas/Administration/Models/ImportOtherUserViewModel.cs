using Erp.BackOffice.App_GlobalResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class ImportOtherUserViewModel
    {
        [Display(Name = "Source", ResourceType = typeof(Wording))]
        public HttpPostedFileBase SourceFile { get; set; }

        public IEnumerable<AddOtherUserImportViewModel> ListUserImport { get; set; }
    }
}