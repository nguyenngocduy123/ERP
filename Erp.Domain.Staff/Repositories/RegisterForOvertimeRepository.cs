using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class RegisterForOvertimeRepository : GenericRepository<ErpStaffDbContext, RegisterForOvertime>, IRegisterForOvertimeRepository
    {
        public RegisterForOvertimeRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all RegisterForOvertime
        /// </summary>
        /// <returns>RegisterForOvertime list</returns>
        public IQueryable<RegisterForOvertime> GetAllRegisterForOvertime()
        {
            return Context.RegisterForOvertime.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwRegisterForOvertime> GetAllvwRegisterForOvertime()
        {
            return Context.vwRegisterForOvertime.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get RegisterForOvertime information by specific id
        /// </summary>
        /// <param name="RegisterForOvertimeId">Id of RegisterForOvertime</param>
        /// <returns></returns>
        public RegisterForOvertime GetRegisterForOvertimeById(int Id)
        {
            return Context.RegisterForOvertime.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwRegisterForOvertime GetvwRegisterForOvertimeById(int Id)
        {
            return Context.vwRegisterForOvertime.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert RegisterForOvertime into database
        /// </summary>
        /// <param name="RegisterForOvertime">Object infomation</param>
        public void InsertRegisterForOvertime(RegisterForOvertime RegisterForOvertime)
        {
            Context.RegisterForOvertime.Add(RegisterForOvertime);
            Context.Entry(RegisterForOvertime).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete RegisterForOvertime with specific id
        /// </summary>
        /// <param name="Id">RegisterForOvertime Id</param>
        public void DeleteRegisterForOvertime(int Id)
        {
            RegisterForOvertime deletedRegisterForOvertime = GetRegisterForOvertimeById(Id);
            Context.RegisterForOvertime.Remove(deletedRegisterForOvertime);
            Context.Entry(deletedRegisterForOvertime).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a RegisterForOvertime with its Id and Update IsDeleted IF that RegisterForOvertime has relationship with others
        /// </summary>
        /// <param name="RegisterForOvertimeId">Id of RegisterForOvertime</param>
        public void DeleteRegisterForOvertimeRs(int Id)
        {
            RegisterForOvertime deleteRegisterForOvertimeRs = GetRegisterForOvertimeById(Id);
            deleteRegisterForOvertimeRs.IsDeleted = true;
            UpdateRegisterForOvertime(deleteRegisterForOvertimeRs);
        }

        /// <summary>
        /// Update RegisterForOvertime into database
        /// </summary>
        /// <param name="RegisterForOvertime">RegisterForOvertime object</param>
        public void UpdateRegisterForOvertime(RegisterForOvertime RegisterForOvertime)
        {
            Context.Entry(RegisterForOvertime).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
