using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Erp.Domain.Repositories
{
    public class PageMenuRepository : GenericRepository<ErpDbContext, PageMenu>, IPageMenuRepository
    {
        public PageMenuRepository(ErpDbContext context)
            : base(context)
        {
        }

        public IEnumerable<vwPageMenu> GetPageMenus(string languageId)
        {
            return Context.vwPageMenu.Where(p => p.LanguageId == languageId);
        }

        public PageMenu GetPageMenuById(int id)
        {
            return Context.PageMenus.Where(p => p.Id == id).FirstOrDefault();
        }

        public void InsertPageMenu(PageMenu pageMenu)
        {
            Context.PageMenus.Add(pageMenu);
            Context.Entry(pageMenu).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeletePageMenu(int id, string languageId)
        {
            PageMenu pageMenu = Context.PageMenus.FirstOrDefault(p => p.Id == id && p.LanguageId == languageId);
            Context.PageMenus.Remove(pageMenu);

            //===== Delete Page Menu in Database ========//

            Context.Entry(pageMenu).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void UpdatePageMenu(PageMenu pageMenu)
        {
            //====== Update Page Menu in Database ======//
            Context.Entry(pageMenu).State = EntityState.Modified;
            Context.SaveChanges();
            
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Page GetPageByAction(string area, string controller, string action)
        {
            var pages = this.Context.Pages.Where(p => p.ControllerName == controller & p.ActionName == action && (p.IsDeleted == null || p.IsDeleted == false));
            if (area != null)
                pages = pages.Where(m => m.AreaName == area);

            return pages.FirstOrDefault();
        }

        public Page GetPageById(int pageId)
        {
            var page = this.Context.Pages.Find(pageId);
            return page;
        }

        public string GetPageName(int pageId, string language)
        {
            var pageMenu = this.Context.PageMenus.FirstOrDefault(p => p.PageId == pageId && p.LanguageId == language);
            return pageMenu != null ? pageMenu.Name : string.Empty;
        }
    }
}
