using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ICalendarVisitDrugStoreRepository
    {
        /// <summary>
        /// Get all CalendarVisitDrugStore
        /// </summary>
        /// <returns>CalendarVisitDrugStore list</returns>
        IQueryable<CalendarVisitDrugStore> GetAllCalendarVisitDrugStore();
        IQueryable<vwCalendarVisitDrugStore> GetvwAllCalendarVisitDrugStore();
        /// <summary>
        /// Get CalendarVisitDrugStore information by specific id
        /// </summary>
        /// <param name="Id">Id of CalendarVisitDrugStore</param>
        /// <returns></returns>
        CalendarVisitDrugStore GetCalendarVisitDrugStoreById(int Id);
        vwCalendarVisitDrugStore GetvwCalendarVisitDrugStoreById(int Id);
        /// <summary>
        /// Insert CalendarVisitDrugStore into database
        /// </summary>
        /// <param name="CalendarVisitDrugStore">Object infomation</param>
        void InsertCalendarVisitDrugStore(CalendarVisitDrugStore CalendarVisitDrugStore);

        /// <summary>
        /// Delete CalendarVisitDrugStore with specific id
        /// </summary>
        /// <param name="Id">CalendarVisitDrugStore Id</param>
        void DeleteCalendarVisitDrugStore(int Id);

        /// <summary>
        /// Delete a CalendarVisitDrugStore with its Id and Update IsDeleted IF that CalendarVisitDrugStore has relationship with others
        /// </summary>
        /// <param name="Id">Id of CalendarVisitDrugStore</param>
        void DeleteCalendarVisitDrugStoreRs(int Id);

        /// <summary>
        /// Update CalendarVisitDrugStore into database
        /// </summary>
        /// <param name="CalendarVisitDrugStore">CalendarVisitDrugStore object</param>
        void UpdateCalendarVisitDrugStore(CalendarVisitDrugStore CalendarVisitDrugStore);
    }
}
