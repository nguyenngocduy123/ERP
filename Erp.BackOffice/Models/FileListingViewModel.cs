using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Models
{
    public class FileListingViewModel
    {
        public List<FileInformation> Files { get; set; }
        public string CKEditorFuncNum { get; set; }
    }
}