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
using System.Data.SqlClient;


namespace Erp.Domain.Repositories
{
    public class PageRepository : GenericRepository<ErpDbContext, Page>, IPageRepository
    {
         public PageRepository(ErpDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Page> GetAllPage()
        {
            return Context.Pages.Where(u => u.IsDeleted != true);
        }

        public IEnumerable<vwPage> GetPages()
        {
            return Context.vwPages.Where(u => u.IsDeleted != true);
        }

        public IEnumerable<Page> GetPagesByParent(int? ParentId)
        {
            return Context.Pages.Where(x => x.ParentId == ParentId).OrderBy(x => x.OrderNo);
        }

        public Page GetPageById(int pageId)
        {
            return Context.Pages.SingleOrDefault(p => p.Id == pageId);
        }

        public int InsertAndScopIdPage(Page page)
        {
            Context.Pages.Add(page);

            Context.Entry(page).State = EntityState.Added;
            Context.SaveChanges();
            return page.Id;
        }

        public void InsertPage(Page page)
        {
            Context.Pages.Add(page);

            //====== Add Page into Database ======//
            Context.Entry(page).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void InsertPageMenu(PageMenu pageMenu)
        {
            Context.PageMenus.Add(pageMenu);

            Context.Entry(pageMenu).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeletePage(int pageId)
        {
            Page page = GetPageById(pageId);
            if (page.IsDeleted == null)
            {
                page.IsDeleted = true;
                Context.Entry(page).State = EntityState.Modified;
                Context.SaveChanges();
            }
            else
            {
                if (!page.IsDeleted.Value)
                {
                    page.IsDeleted = true;
                    Context.Entry(page).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
        }

        public void UpdatePage(Page page)
        {
            //====== Update Page in Database ======//
            Context.Entry(page).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void UpdatePageMenu(PageMenu pageMenu)
        {
            Context.Entry(pageMenu).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public vwPage GetPageByAcctionController(string action, string controller)
        {
            return Context.vwPages.Where(item => item.ActionName.Trim().ToLower() == action.Trim().ToLower() && item.ControllerName.Trim().ToLower() == controller.Trim().ToLower() && (item.IsDeleted == null || item.IsDeleted == false)).FirstOrDefault();
        }

        public IEnumerable<vwPage> GetPagesAccessRight(int userId, int userTypeId)
        {
            return Context.vwPages.SqlQuery("GetPagesAccessRight {0}, {1}", userId, userTypeId).AsEnumerable();
        }
    }
}
