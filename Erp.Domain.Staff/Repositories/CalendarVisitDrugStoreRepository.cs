using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class CalendarVisitDrugStoreRepository : GenericRepository<ErpStaffDbContext, CalendarVisitDrugStore>, ICalendarVisitDrugStoreRepository
    {
        public CalendarVisitDrugStoreRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CalendarVisitDrugStore
        /// </summary>
        /// <returns>CalendarVisitDrugStore list</returns>
        public IQueryable<CalendarVisitDrugStore> GetAllCalendarVisitDrugStore()
        {
            return Context.CalendarVisitDrugStore.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwCalendarVisitDrugStore> GetvwAllCalendarVisitDrugStore()
        {
            return Context.vwCalendarVisitDrugStore.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get CalendarVisitDrugStore information by specific id
        /// </summary>
        /// <param name="CalendarVisitDrugStoreId">Id of CalendarVisitDrugStore</param>
        /// <returns></returns>
        public CalendarVisitDrugStore GetCalendarVisitDrugStoreById(int Id)
        {
            return Context.CalendarVisitDrugStore.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwCalendarVisitDrugStore GetvwCalendarVisitDrugStoreById(int Id)
        {
            return Context.vwCalendarVisitDrugStore.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert CalendarVisitDrugStore into database
        /// </summary>
        /// <param name="CalendarVisitDrugStore">Object infomation</param>
        public void InsertCalendarVisitDrugStore(CalendarVisitDrugStore CalendarVisitDrugStore)
        {
            Context.CalendarVisitDrugStore.Add(CalendarVisitDrugStore);
            Context.Entry(CalendarVisitDrugStore).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CalendarVisitDrugStore with specific id
        /// </summary>
        /// <param name="Id">CalendarVisitDrugStore Id</param>
        public void DeleteCalendarVisitDrugStore(int Id)
        {
            CalendarVisitDrugStore deletedCalendarVisitDrugStore = GetCalendarVisitDrugStoreById(Id);
            Context.CalendarVisitDrugStore.Remove(deletedCalendarVisitDrugStore);
            Context.Entry(deletedCalendarVisitDrugStore).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CalendarVisitDrugStore with its Id and Update IsDeleted IF that CalendarVisitDrugStore has relationship with others
        /// </summary>
        /// <param name="CalendarVisitDrugStoreId">Id of CalendarVisitDrugStore</param>
        public void DeleteCalendarVisitDrugStoreRs(int Id)
        {
            CalendarVisitDrugStore deleteCalendarVisitDrugStoreRs = GetCalendarVisitDrugStoreById(Id);
            deleteCalendarVisitDrugStoreRs.IsDeleted = true;
            UpdateCalendarVisitDrugStore(deleteCalendarVisitDrugStoreRs);
        }

        /// <summary>
        /// Update CalendarVisitDrugStore into database
        /// </summary>
        /// <param name="CalendarVisitDrugStore">CalendarVisitDrugStore object</param>
        public void UpdateCalendarVisitDrugStore(CalendarVisitDrugStore CalendarVisitDrugStore)
        {
            Context.Entry(CalendarVisitDrugStore).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
