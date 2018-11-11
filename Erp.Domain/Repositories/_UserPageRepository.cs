using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Repositories
{
    public class UserPageRepository : GenericRepository<ErpDbContext, UserPage>, IUserPageRepository
    {
        public UserPageRepository(ErpDbContext context)
            : base(context)
        {

        }

        public IEnumerable<UserPage> GetAllItem()
        {
            return Context.UserPages.AsEnumerable();
        }

        public IEnumerable<UserPage> GetAllItemByUserID(int userId)
        {
            return Context.UserPages.Where(x => x.UserId == userId);
        }

        public IEnumerable<UserPage> GetAllItemByPageID(int pageId)
        {
            return Context.UserPages.Where(x => x.PageId == pageId);
        }

        public UserPage GetByUserIdPageId(int userId, int pageId)
        {
            return Context.UserPages.SingleOrDefault(q => q.UserId == userId && q.PageId == pageId);
        }

        public void Insert(UserPage obj)
        {
            Context.UserPages.Add(obj);

            //====== Add into Database ======//
            Context.Entry(obj).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void Delete(int userId, int pageId)
        {
            //====== Delete in Database ======//
            Context.UserPages.Remove(GetByUserIdPageId(userId, pageId));
            Context.Entry(GetByUserIdPageId(userId, pageId)).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void Delete(int userId)
        {
            ////====== Delete in Database ======//
            List<UserPage> userPages = GetAllItemByUserID(userId).ToList();
            foreach (UserPage item in userPages)
            {
                Context.UserPages.Remove(item);
                Context.Entry(item).State = EntityState.Deleted;
            }
            Context.SaveChanges();
            //Context.Database.ExecuteSqlCommand("UserPage_DeleteByUserId {0}", userId);
        }

        public void Update(UserPage obj)
        {
            //====== Update in Database ======//
            Context.Entry(obj).State = EntityState.Modified;
            Context.SaveChanges();
        }


        public int DeleteAll()
        {
            return Context.Database.ExecuteSqlCommand("UserPage_DeleteAll");
        }


        public int DeleteAllByUserId(int userId)
        {
            return Context.Database.ExecuteSqlCommand("UserPage_DeleteAllByUserId {0}", userId);
        }
    }
}
