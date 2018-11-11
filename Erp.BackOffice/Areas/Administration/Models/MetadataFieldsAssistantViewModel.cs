using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Administration.Models
{
    public class MetadataFieldsAssistantViewModel
    {
        public string Entity { get; set; }

        public string MetadataField { get; set; }
        public SelectList SelectListMetadataFields { get; set; }
    }
}