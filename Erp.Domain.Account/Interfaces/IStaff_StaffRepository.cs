using Erp.Domain.Account.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Interfaces
{
    public interface IStaff_StaffRepository
    {
        /// <summary>
        /// Get all staff
        /// </summary>
        /// <returns></returns>
        IQueryable<Staff_Staff> GetAllStaff();
        IQueryable<vwStaff_Staff> GetAllvwStaff();
        IEnumerable<Staff_Staff> GetAllStaff1();
        IEnumerable<vwStaff_Staff> GetAllvwStaff1();
        /// <summary>
        /// Get staff information by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Staff_Staff GetStaffById(int Id);
        vwStaff_Staff GetvwStaffById(int Id);
    }
}
