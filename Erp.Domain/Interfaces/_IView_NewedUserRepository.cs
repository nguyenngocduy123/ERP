using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Interfaces
{
    public interface IView_NewedUserRepository
    {
        IEnumerable<News_ViewedUser> GetAlls();
        IEnumerable<News_ViewedUser> GetbyUserId(int UserId);
        News_ViewedUser GetNews_ViewedUsers(int UserId, int NewId);
        void Insert(News_ViewedUser newviewuser);
    }
}
