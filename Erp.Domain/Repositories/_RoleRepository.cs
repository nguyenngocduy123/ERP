using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;

namespace Erp.Domain.Repositories
{
    public class RoleRepository : GenericRepository<ErpDbContext, Role>, IRoleRepository
    {
        public RoleRepository(ErpDbContext context): base(context)
        {
        }

        #region IRoleRepository Members

        public IEnumerable<Entities.Role> GetRoles()
        {
            throw new NotImplementedException();
            //return Context.Role.AsEnumerable();
        }

        public Entities.Role GetRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public void InsertRole(Entities.Role role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(Entities.Role role)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
