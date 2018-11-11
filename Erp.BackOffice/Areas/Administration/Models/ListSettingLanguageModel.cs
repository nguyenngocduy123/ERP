using Erp.Domain.Entities;
using System.Collections.Generic;

namespace Erp.BackOffice.Administration.Models
{
    public class ListSettingLanguageModel
    {
        public IEnumerable<Language> languages { get; set; }
    }
}