using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Repositories
{
    public class SettingLanguageRepository : GenericRepository<ErpDbContext, Language>, ISettingLanguageRepository
    {
        public SettingLanguageRepository(ErpDbContext context)
            : base(context)
        {
        }


        public IEnumerable<Language> GetLanguages()
        {
            return Context.Languages.Where(u => u.Id != null).ToList();
        }

        public Language GetLanguageById(string languageId)
        {
            return Context.Languages.SingleOrDefault(u => u.Id == languageId);
        }

        public string GetDefaultLanguage()
        {
            Language defaultLanguage = GetLanguages().SingleOrDefault(u => u.IsDefault == true);
            return defaultLanguage.Id;
        }

        public void UpdateOtherLanguage(string languageId)
        {
            List<Language> listLanguage = Context.Languages.Where(u => u.Id != languageId).AsEnumerable().ToList();
            if (listLanguage != null && listLanguage.Count > 0)
            {
                for (int i = 0; i < listLanguage.Count; i++)
                {
                    Language otherLanguage = GetLanguageById(listLanguage[i].Id);
                    if (otherLanguage.IsDefault == true)
                    {
                        otherLanguage.IsDefault = false;
                        UpdateLanguage(otherLanguage);
                    }
                }
            }
            
        }

        public void ChooseDefaultOtherLanguage(string languageId)
        {
            List<Language> listLanguage = Context.Languages.Where(u => u.Id != languageId).AsEnumerable().ToList();
            if (listLanguage != null && listLanguage.Count > 0)
            {
                bool isDefault = false;
                for (int i = 0; i < listLanguage.Count; i++)
                {
                    Language otherlanguage = GetLanguageById(listLanguage[i].Id);
                    if (otherlanguage.IsDefault == true)
                    {
                        isDefault = true;
                        break;
                    }
                }
                if (!isDefault)
                {
                    Language firstLanguage = GetLanguageById(listLanguage[0].Id);
                    firstLanguage.IsDefault = true;
                    UpdateLanguage(firstLanguage);
                }
            }
        }

        public void InsertLanguage(Language language)
        {
            Context.Languages.Add(language);
            Context.Entry(language).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void UpdateLanguage(Language language)
        {
            Context.Entry(language).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteLanguage(string languageId)
        {
            Context.Languages.Remove(GetLanguageById(languageId));

            Language language = GetLanguageById(languageId);
            Context.Entry(language).State = EntityState.Deleted;
            Context.SaveChanges();
            
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
