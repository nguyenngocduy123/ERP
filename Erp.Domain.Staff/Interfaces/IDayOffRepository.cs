using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDayOffRepository
    {
        /// <summary>
        /// Get all DayOff
        /// </summary>
        /// <returns>DayOff list</returns>
        IQueryable<DayOff> GetAllDayOff();
        IQueryable<vwDayOff> GetAllvwDayOff();
        /// <summary>
        /// Get DayOff information by specific id
        /// </summary>
        /// <param name="Id">Id of DayOff</param>
        /// <returns></returns>
        DayOff GetDayOffById(int Id);
        vwDayOff GetvwDayOffById(int Id);
        /// <summary>
        /// Insert DayOff into database
        /// </summary>
        /// <param name="DayOff">Object infomation</param>
        void InsertDayOff(DayOff DayOff);

        /// <summary>
        /// Delete DayOff with specific id
        /// </summary>
        /// <param name="Id">DayOff Id</param>
        void DeleteDayOff(int Id);

        /// <summary>
        /// Delete a DayOff with its Id and Update IsDeleted IF that DayOff has relationship with others
        /// </summary>
        /// <param name="Id">Id of DayOff</param>
        void DeleteDayOffRs(int Id);

        /// <summary>
        /// Update DayOff into database
        /// </summary>
        /// <param name="DayOff">DayOff object</param>
        void UpdateDayOff(DayOff DayOff);
    }
}
