using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class LoginLogRepository : GenericRepository<ErpDbContext, LoginLog>, ILoginLogRepository
    {
        public LoginLogRepository(ErpDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all answers from database
        /// </summary>
        /// <returns>Object</returns>
        //lam?
        public IEnumerable<LoginLog> GetAllLoginLog()
        {
            return Context.LoginLogs.AsQueryable();
        }
        
        public void InsertLoginLog(LoginLog loginlog)
        {
            Context.LoginLogs.Add(loginlog);
            Context.SaveChanges();
        }



    }
}
