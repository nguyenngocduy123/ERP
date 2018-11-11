using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Repositories
{
    public class Staff_StaffRepository : GenericRepository<ErpAccountDbContext, Staff_Staff>, IStaff_StaffRepository
    {
        public Staff_StaffRepository(ErpAccountDbContext context) : base(context)
        {

        }

        public IQueryable<Staff_Staff> GetAllStaff()
        {
            return Context.Staff_Staff.Where(x => (x.IsDeleted == null || x.IsDeleted == false));
        }

        public IQueryable<vwStaff_Staff> GetAllvwStaff()
        {
            return Context.vwStaff_Staff.Where(x => (x.IsDeleted == null || x.IsDeleted == false));
        }
        public IEnumerable<Staff_Staff> GetAllStaff1()
        {
            return Context.Staff_Staff.Where(x => (x.IsDeleted == null || x.IsDeleted == false));
        }
        public IEnumerable<vwStaff_Staff> GetAllvwStaff1()
        {
            return Context.vwStaff_Staff.Where(x => (x.IsDeleted == null || x.IsDeleted == false));
        }
        public Staff_Staff GetStaffById(int Id)
        {
            return Context.Staff_Staff.SingleOrDefault(x => x.Id == Id && (x.IsDeleted == null || x.IsDeleted == false));
        }

        public vwStaff_Staff GetvwStaffById(int Id)
        {
            return Context.vwStaff_Staff.SingleOrDefault(x => x.Id == Id && (x.IsDeleted == null || x.IsDeleted == false));
        }
    }
}
