using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IHolidaysRepository
    {
        /// <summary>
        /// Get all Holidays
        /// </summary>
        /// <returns>Holidays list</returns>
        IQueryable<Holidays> GetAllHolidays();

        /// <summary>
        /// Get Holidays information by specific id
        /// </summary>
        /// <param name="Id">Id of Holidays</param>
        /// <returns></returns>
        Holidays GetHolidaysById(int Id);

        /// <summary>
        /// Insert Holidays into database
        /// </summary>
        /// <param name="Holidays">Object infomation</param>
        void InsertHolidays(Holidays Holidays);

        /// <summary>
        /// Delete Holidays with specific id
        /// </summary>
        /// <param name="Id">Holidays Id</param>
        void DeleteHolidays(int Id);

        /// <summary>
        /// Delete a Holidays with its Id and Update IsDeleted IF that Holidays has relationship with others
        /// </summary>
        /// <param name="Id">Id of Holidays</param>
        void DeleteHolidaysRs(int Id);

        /// <summary>
        /// Update Holidays into database
        /// </summary>
        /// <param name="Holidays">Holidays object</param>
        void UpdateHolidays(Holidays Holidays);
    }
}
