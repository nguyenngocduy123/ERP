using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Data.Entity;

namespace Erp.Domain.Repositories
{
    public class UserTypeRepository : GenericRepository<ErpDbContext, UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ErpDbContext context)
            : base(context)
        {
        }

        #region IUserTypeRepository Members

        public IQueryable<Entities.UserType> GetUserTypes()
        {
            return Context.UserType.Where(u => u.Id != 0).OrderBy(u => u.OrderNo);
        }

        public Entities.UserType GetUserTypeById(int userTypeId)
        {
            if (userTypeId == 0)
            {
                return null;
            }
            return Context.UserType.FirstOrDefault(u => u.Id == userTypeId);
        }
        public Entities.UserType GetUserTypeByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            return Context.UserType.FirstOrDefault(u => u.Code == code);
        }
        public void InsertUserType(Entities.UserType userType)
        {
            Context.UserType.Add(userType);

            //=========== Database ======//
            Context.Entry(userType).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteUserType(int userTypeId)
        {
            UserType userType = Context.UserType.FirstOrDefault(u => u.Id == userTypeId);
            Context.UserType.Remove(userType);

            //===== Database ========//

            Context.Entry(userType).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void UpdateUserType(Entities.UserType userType)
        {
            Context.Entry(userType).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        #endregion
    }
}
