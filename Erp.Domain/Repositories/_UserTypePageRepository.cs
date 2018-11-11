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
    public class UserTypePageRepository : GenericRepository<ErpDbContext, UserTypePage>, IUserTypePageRepository
    {
        public UserTypePageRepository(ErpDbContext context)
            : base(context)
        {

        }

        public IEnumerable<UserTypePage> GetAllItem()
        {
            return Context.UserTypePages.AsEnumerable();
        }

        public IEnumerable<UserTypePage> GetAllItemByPageId(int pageId)
        {
            return Context.UserTypePages.Where(x => x.PageId == pageId);
        }

        public IEnumerable<UserTypePage> GetAllItemByUserTypeID(int userTypeId)
        {
            return Context.UserTypePages.Where(x => x.UserTypeId == userTypeId);
        }

        public UserTypePage GetByUserTypeIdPageId(int userTypeId, int pageId)
        {
            return Context.UserTypePages.SingleOrDefault(q => q.UserTypeId == userTypeId && q.PageId == pageId);
        }

        public void Insert(UserTypePage obj)
        {
            Context.UserTypePages.Add(obj);

            //====== Add into Database ======//
            Context.Entry(obj).State = EntityState.Added;
            Context.SaveChanges();
            //Context.Entry<UserTypePage>(obj).Reload();
        }

        public void Delete(int userTypeId, int pageId)
        {
            //====== Delete in Database ======//
            Context.UserTypePages.Remove(GetByUserTypeIdPageId(userTypeId, pageId));
            Context.Entry(GetByUserTypeIdPageId(userTypeId, pageId)).State = EntityState.Deleted;
            Context.SaveChanges();
            //Context.Entry<UserTypePage>(GetByUserTypeIdPageId(userTypeId, pageId)).Reload();
        }

        public void Update(UserTypePage obj)
        {
            //====== Update in Database ======//
            Context.Entry(obj).State = EntityState.Modified;
            Context.SaveChanges(); 
            //Context.Entry<UserTypePage>(obj).Reload();
        }

        public int DeleteAll()
        {
            return Context.Database.ExecuteSqlCommand("UserTypePage_DeleteAll");
        }       
    }
}
