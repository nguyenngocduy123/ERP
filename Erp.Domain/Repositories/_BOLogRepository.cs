using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Repositories
{
    using Erp.Domain.Entities;
    using Erp.Domain.Interfaces;

    public class BOLogRepository : GenericRepository<ErpDbContext, BOLog>, IBOLogRepositoty
    {
        public BOLogRepository(ErpDbContext context)
            : base(context)
        {
            
        }

        public void SaveBOLog(BOLog boLog)
        {
            var user = this.Context.Users.FirstOrDefault(u => u.UserName == boLog.UserName);
            if (user != null)
            {
                boLog.UserId = user.Id;
                Context.BOLogs.Add(boLog);
                Context.SaveChanges();
            }
        }

        public IQueryable<BOLog> GetAllBOLog()
        {
            return Context.BOLogs.OrderByDescending(b => b.CreatedDate);
        }
    }
}
