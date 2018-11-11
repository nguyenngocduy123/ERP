using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Interfaces
{
    public interface ISettingLanguageRepository
    {
        IEnumerable<Language> GetLanguages();
        Language GetLanguageById(string languageId);
        string GetDefaultLanguage();
        void UpdateOtherLanguage(string languageId);
        void ChooseDefaultOtherLanguage(string languageId);
        void InsertLanguage(Language language);
        void UpdateLanguage(Language language);
        void DeleteLanguage(string languageId);
        void Save();
    }
}
