using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Interfaces
{
    public interface IUserPageRepository
    {
        IEnumerable<UserPage> GetAllItem();
        IEnumerable<UserPage> GetAllItemByUserID(int userId);
        IEnumerable<UserPage> GetAllItemByPageID(int pageId);
        //IEnumerable<UserTypePage> GetAllViewItem();
        UserPage GetByUserIdPageId(int userId, int pageId);
        void Insert(UserPage obj);
        void Delete(int userId);
        void Delete(int userId, int pageId);
        int DeleteAll();
        int DeleteAllByUserId(int userId);
        void Update(UserPage obj);
    }
}
