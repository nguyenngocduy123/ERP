using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IShiftsRepository
    {
        /// <summary>
        /// Get all Shifts
        /// </summary>
        /// <returns>Shifts list</returns>
        IQueryable<Shifts> GetAllShifts();

        /// <summary>
        /// Get Shifts information by specific id
        /// </summary>
        /// <param name="Id">Id of Shifts</param>
        /// <returns></returns>
        Shifts GetShiftsById(int Id);

        /// <summary>
        /// Insert Shifts into database
        /// </summary>
        /// <param name="Shifts">Object infomation</param>
        void InsertShifts(Shifts Shifts);

        /// <summary>
        /// Delete Shifts with specific id
        /// </summary>
        /// <param name="Id">Shifts Id</param>
        void DeleteShifts(int Id);

        /// <summary>
        /// Delete a Shifts with its Id and Update IsDeleted IF that Shifts has relationship with others
        /// </summary>
        /// <param name="Id">Id of Shifts</param>
        void DeleteShiftsRs(int Id);

        /// <summary>
        /// Update Shifts into database
        /// </summary>
        /// <param name="Shifts">Shifts object</param>
        void UpdateShifts(Shifts Shifts);
    }
}
