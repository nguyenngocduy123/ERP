using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Data.Entity.Validation;
using System.Globalization;
//using System.Data.Objects.SqlClient;

namespace Erp.Domain.Repositories
{
    public class UserRepository : GenericRepository<ErpDbContext, User>, IUserRepository
    {
        public UserRepository(ErpDbContext context)
            : base(context)
        {
        }
    

        #region IUsersRepository Members
        public IEnumerable<User> GetUsers()
        {
            return Context.Users.Where(u => u.UserName != null).AsEnumerable().OrderByDescending(u => u.Status);
        }
        public IQueryable<vwUsers> GetUserbyUserType(string typename)
        {
            return Context.vwUsers.Where(item => item.UserTypeName.Trim().ToLower() == typename.Trim().ToLower());
        }
        public IQueryable<User> GetAllUsers()
        {
            return Context.Users.Where(u => u.UserName != null);
        }
        public IQueryable<vwUsers> GetAllvwUsers()
        {
            return Context.vwUsers.Where(u => u.UserName != null);
        }
        public IQueryable<vwUsers> GetUsersAvailable(int? CategoryId = null, int? TypeId = null, int? StatusId = null, string txtSearch = null)
        {
            string a = "Admin".ToUpper();
            string h = "Host".ToUpper();
            UserStatus us = UserStatus.Active;
            if (StatusId.HasValue)
            {
                if (StatusId == 0)
                    us = UserStatus.Deleted;
                if (StatusId == 1)
                    us = UserStatus.Active;
                if (StatusId == -1)
                    us = UserStatus.Pending;

            }
            return Context.vwUsers.Where(u => u.UserName != null && u.Email != null
                && (u.UserName.ToUpper() != a)
                && (u.UserName.ToUpper() != h)
                && (string.IsNullOrEmpty(txtSearch) || u.UserName.ToUpper().Contains(txtSearch.ToUpper()))
                && (StatusId == null || u.Status == us)
                && (TypeId == null || u.UserTypeId == TypeId)
                ).OrderByDescending(m => m.ModifiedDate);
        }
        public User GetByUserName(string userName)
        {
           return Context.Users.FirstOrDefault(u => u.UserName.Trim().ToLower() == userName.Trim().ToLower());
        }
        public vwUsers GetByvwUserName(string userName)
        {
            return Context.vwUsers.FirstOrDefault(u => u.UserName.Trim().ToLower() == userName.Trim().ToLower());
        }
        public User GetByUserEmail(string email)
        {
            return Context.Users.FirstOrDefault(u => u.Email.Trim().ToLower() == email.Trim().ToLower());
        }
        public string GetFirstNameByUserName(string userName)
        {
            return Context.Users.Where(u => u.UserName.Trim().ToLower() == userName.Trim().ToLower()).Select(x => x.FirstName).FirstOrDefault();
        }

        public User GetUserById(int userId)
        {
            return Context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
        public vwUsers GetvwUserById(int userId)
        {
            return Context.vwUsers.Where(u => u.Id == userId).FirstOrDefault();
        }
        public int GetUserTypeId(int userId)
        {
            return Context.Users.Where(item => item.Id == userId).Select(item => item.UserTypeId.Value).FirstOrDefault();
        }

        public void InsertUser(User user)
        {
            Context.Users.Add(user);

            //====== Add User into Database ======//
            Context.Entry(user).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            User deletedUser = Context.Users.FirstOrDefault(x => x.Id == userId);
            //deletedUser.Status = (int)UserStatus.Deleted;
            //deletedUser.ModifiedDate = DateTime.Now;

            //====== Delete User in Database ======//
            Context.Users.Remove(deletedUser);
            Context.Entry(deletedUser).State = EntityState.Deleted;

            //Context.Entry(deletedUser).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public int increaseLoginFailed(int userId)
        {
            User deletedUser = GetUserById(userId);
            if (deletedUser == null)
                return 0;
            if (deletedUser.LoginFailedCount == null)
                deletedUser.LoginFailedCount = 0;
            int current_count = (int)deletedUser.LoginFailedCount;

            deletedUser.LoginFailedCount = current_count + 1;
            deletedUser.ModifiedDate = DateTime.Now;
            Context.Entry(deletedUser).State = EntityState.Modified;
            Context.SaveChanges();
            return current_count + 1;
        }

        public void resetLoginFailed(int userId)
        {
            User deletedUser = GetUserById(userId);
            deletedUser.LoginFailedCount = 0;
            deletedUser.ModifiedDate = DateTime.Now;
            Context.Entry(deletedUser).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            if (user.Id == null)
            {
                Context.Users.Add(user);
            }
            else
            {
                Context.Entry(user).State = EntityState.Modified;
            }

            Context.SaveChanges();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public int GetUserByEmailAndCategoryName(string email, string categoryName)
        {
            var query = Context.Users.Where(u => (u.Email == email)).FirstOrDefault();

            return query != null ? query.Id : -1;
        }

        #endregion
    }
}
