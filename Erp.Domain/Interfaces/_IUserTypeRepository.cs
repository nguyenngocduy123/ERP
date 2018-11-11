using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;

namespace Erp.Domain.Interfaces
{
    public interface IUserTypeRepository
    {
        IQueryable<UserType> GetUserTypes();
        UserType GetUserTypeById(int userTypeId);
        void InsertUserType(UserType userType);
        void DeleteUserType(int userTypeId);
        void UpdateUserType(UserType userType);
        void Save();

        UserType GetUserTypeByCode(string code);
    }
}
