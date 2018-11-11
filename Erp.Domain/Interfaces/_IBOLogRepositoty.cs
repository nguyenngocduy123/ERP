using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Interfaces
{
    using Erp.Domain.Entities;

    public interface IBOLogRepositoty
    {
        void SaveBOLog(BOLog boLog);

        IQueryable<BOLog> GetAllBOLog();
    }
}
