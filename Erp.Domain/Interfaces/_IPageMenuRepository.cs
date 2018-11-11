using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;

namespace Erp.Domain.Interfaces
{
    public interface IPageMenuRepository
    {
        IEnumerable<vwPageMenu> GetPageMenus(string languageId);
        PageMenu GetPageMenuById(int id);
        void InsertPageMenu(PageMenu pageMenu);
        void DeletePageMenu(int id, string languageId);
        void UpdatePageMenu(PageMenu pageMenu);
        void Save();
        Page GetPageByAction(string area, string controller, string action);
        Page GetPageById(int pageId);
        string GetPageName(int pageId, string language);
    }
}
