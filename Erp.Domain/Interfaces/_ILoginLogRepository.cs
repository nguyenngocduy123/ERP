using Erp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Erp.Domain.Interfaces
{
    public interface ILoginLogRepository
    {
         IEnumerable<LoginLog> GetAllLoginLog();
         void InsertLoginLog(LoginLog loginlog);

        
    }
}
