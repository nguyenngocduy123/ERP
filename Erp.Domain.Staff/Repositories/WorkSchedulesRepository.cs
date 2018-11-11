using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class WorkSchedulesRepository : GenericRepository<ErpStaffDbContext, WorkSchedules>, IWorkSchedulesRepository
    {
        public WorkSchedulesRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all WorkSchedules
        /// </summary>
        /// <returns>WorkSchedules list</returns>
        public IQueryable<WorkSchedules> GetAllWorkSchedules()
        {
            return Context.WorkSchedules.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwWorkSchedules> GetvwAllWorkSchedules()
        {
            return Context.vwWorkSchedules.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get WorkSchedules information by specific id
        /// </summary>
        /// <param name="WorkSchedulesId">Id of WorkSchedules</param>
        /// <returns></returns>
        public WorkSchedules GetWorkSchedulesById(int Id)
        {
            return Context.WorkSchedules.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwWorkSchedules GetvwWorkSchedulesById(int Id)
        {
            return Context.vwWorkSchedules.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert WorkSchedules into database
        /// </summary>
        /// <param name="WorkSchedules">Object infomation</param>
        public void InsertWorkSchedules(WorkSchedules WorkSchedules)
        {
            Context.WorkSchedules.Add(WorkSchedules);
            Context.Entry(WorkSchedules).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete WorkSchedules with specific id
        /// </summary>
        /// <param name="Id">WorkSchedules Id</param>
        public void DeleteWorkSchedules(int Id)
        {
            WorkSchedules deletedWorkSchedules = GetWorkSchedulesById(Id);
            Context.WorkSchedules.Remove(deletedWorkSchedules);
            Context.Entry(deletedWorkSchedules).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a WorkSchedules with its Id and Update IsDeleted IF that WorkSchedules has relationship with others
        /// </summary>
        /// <param name="WorkSchedulesId">Id of WorkSchedules</param>
        public void DeleteWorkSchedulesRs(int Id)
        {
            WorkSchedules deleteWorkSchedulesRs = GetWorkSchedulesById(Id);
            deleteWorkSchedulesRs.IsDeleted = true;
            UpdateWorkSchedules(deleteWorkSchedulesRs);
        }

        /// <summary>
        /// Update WorkSchedules into database
        /// </summary>
        /// <param name="WorkSchedules">WorkSchedules object</param>
        public void UpdateWorkSchedules(WorkSchedules WorkSchedules)
        {
            Context.Entry(WorkSchedules).State = EntityState.Modified;
            Context.SaveChanges();
        }
        public WorkSchedules GetByStaffAndShifts(string Day, int StaffId, int ShiftsId)
        {
            var single = Context.WorkSchedules.AsEnumerable().SingleOrDefault(c => c.Day.Value.ToString("dd/MM/yyyy") == Day && c.StaffId == StaffId && c.ShiftsId == ShiftsId);

            return single;
        }
        public void Delete(string Day, int StaffId, int ShiftsId)
        {
            WorkSchedules item = GetByStaffAndShifts(Day, StaffId, ShiftsId);
            Context.WorkSchedules.Remove(item);
            Context.Entry(item).State = EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}
