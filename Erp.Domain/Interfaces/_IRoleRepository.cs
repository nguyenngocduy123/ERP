using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;

namespace Erp.Domain.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        Role GetRoleById(int roleId);
        void InsertRole(Role role);
        void DeleteRole(int roleId);
        void UpdateRole(Role role);
        void Save();
    }
}
