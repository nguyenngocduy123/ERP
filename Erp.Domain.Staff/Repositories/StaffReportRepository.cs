using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class StaffReportRepository : GenericRepository<ErpStaffDbContext, Staffs>, IStaffReportRepository
    {
        public StaffReportRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

     
    }
}
