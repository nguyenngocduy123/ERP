using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;

namespace Erp.Domain.Interfaces
{
    public interface IPageRepository 
    {
        IEnumerable<Page> GetAllPage();
        IEnumerable<vwPage> GetPages();
        IEnumerable<Page> GetPagesByParent(int? ParentId);
        Page GetPageById(int pageId);
        int InsertAndScopIdPage(Page page);
        void InsertPage(Page page);
        void InsertPageMenu(PageMenu pageMenu);
        void DeletePage(int pageId);
        void UpdatePage(Page page);
        void UpdatePageMenu(PageMenu pageMenu);
        void Save();
        vwPage GetPageByAcctionController(string action, string controller);
        IEnumerable<vwPage> GetPagesAccessRight(int userId, int userTypeId);
    }
}
