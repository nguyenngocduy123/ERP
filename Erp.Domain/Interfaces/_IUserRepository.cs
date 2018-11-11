using System.Collections.Generic;
using Erp.Domain.Entities;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IQueryable<User> GetAllUsers();
        IQueryable<vwUsers> GetAllvwUsers();
        IQueryable<vwUsers> GetUserbyUserType(string typename);
        IQueryable<vwUsers> GetUsersAvailable(int? CategoryId = null, int? TypeId = null, int? StatusId = null, string txtSearch = null);
        User GetUserById(int userId);
        vwUsers GetvwUserById(int userId);
        User GetByUserName(string userName);
        vwUsers GetByvwUserName(string userName);
        User GetByUserEmail(string email);
        string GetFirstNameByUserName(string email);
        void InsertUser(User user);
        void DeleteUser(int userId);
        int increaseLoginFailed(int userId);
        void resetLoginFailed(int userId);
        void UpdateUser(User user);
        void Save();
        int GetUserByEmailAndCategoryName(string email, string categoryName);
    }
}
