using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DayOffRepository : GenericRepository<ErpStaffDbContext, DayOff>, IDayOffRepository
    {
        public DayOffRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DayOff
        /// </summary>
        /// <returns>DayOff list</returns>
        public IQueryable<DayOff> GetAllDayOff()
        {
            return Context.DayOff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwDayOff> GetAllvwDayOff()
        {
            return Context.vwDayOff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get DayOff information by specific id
        /// </summary>
        /// <param name="DayOffId">Id of DayOff</param>
        /// <returns></returns>
        public DayOff GetDayOffById(int Id)
        {
            return Context.DayOff.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwDayOff GetvwDayOffById(int Id)
        {
            return Context.vwDayOff.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert DayOff into database
        /// </summary>
        /// <param name="DayOff">Object infomation</param>
        public void InsertDayOff(DayOff DayOff)
        {
            Context.DayOff.Add(DayOff);
            Context.Entry(DayOff).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DayOff with specific id
        /// </summary>
        /// <param name="Id">DayOff Id</param>
        public void DeleteDayOff(int Id)
        {
            DayOff deletedDayOff = GetDayOffById(Id);
            Context.DayOff.Remove(deletedDayOff);
            Context.Entry(deletedDayOff).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DayOff with its Id and Update IsDeleted IF that DayOff has relationship with others
        /// </summary>
        /// <param name="DayOffId">Id of DayOff</param>
        public void DeleteDayOffRs(int Id)
        {
            DayOff deleteDayOffRs = GetDayOffById(Id);
            deleteDayOffRs.IsDeleted = true;
            UpdateDayOff(deleteDayOffRs);
        }

        /// <summary>
        /// Update DayOff into database
        /// </summary>
        /// <param name="DayOff">DayOff object</param>
        public void UpdateDayOff(DayOff DayOff)
        {
            Context.Entry(DayOff).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
