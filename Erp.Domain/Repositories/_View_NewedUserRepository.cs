// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LessonFoRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The lesson fo repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Erp.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Erp.Domain.Entities;
    using Erp.Domain.Interfaces;
    using Erp.Utilities;

    /// <summary>
    /// The lesson repository.
    /// </summary>
    public class View_NewedUserRepository : GenericRepository<ErpDbContext, News_ViewedUser>, IView_NewedUserRepository
    {

        public View_NewedUserRepository(ErpDbContext context)
            : base(context)
        {

        }


        public IEnumerable<News_ViewedUser> GetAlls()
        {
            return Context.News_ViewedUsers.AsEnumerable();
        }
        public IEnumerable<News_ViewedUser> GetbyUserId(int UserId)
        {
            return Context.News_ViewedUsers.AsEnumerable().Where(a=>a.ViewedUser==UserId);
        }
        public News_ViewedUser GetNews_ViewedUsers(int UserId,int NewId)
        {
            return Context.News_ViewedUsers.Where(m =>(m.ViewedUser== UserId && m.NewsId==NewId) ).FirstOrDefault();
        }

        public void Insert(News_ViewedUser new_view)
        {
            var newview = GetNews_ViewedUsers(new_view.ViewedUser, new_view.NewsId);
            if (newview == null)
            {
                Context.News_ViewedUsers.Add(new_view);
            }
            else
            {
                newview.ViewCount = newview.ViewCount + 1;
                Context.Entry(newview).State = EntityState.Modified;
            }
            //Context.Entry(new_view).State = System.Data.EntityState.Added;
            Context.SaveChanges();
        }

    }
}