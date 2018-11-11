using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITimekeepingRepository
    {
        /// <summary>
        /// Get all Timekeeping
        /// </summary>
        /// <returns>Timekeeping list</returns>
        IQueryable<Timekeeping> GetAllTimekeeping();
        IQueryable<vwTimekeeping> GetvwAllTimekeeping();
        IQueryable<vwTotalTimekeeping> GetAllvwTimekeepingSynthesis();
        /// <summary>
        /// Get Timekeeping information by specific id
        /// </summary>
        /// <param name="Id">Id of Timekeeping</param>
        /// <returns></returns>
        Timekeeping GetTimekeepingById(int Id);
        vwTimekeeping GetvwTimekeepingById(int TimekeepingId);
        vwTimekeeping GetvwTimekeepingByWorkSchedulesId(int WorkSchedulesId);
        /// <summary>
        /// Insert Timekeeping into database
        /// </summary>
        /// <param name="Timekeeping">Object infomation</param>
        void InsertTimekeeping(Timekeeping Timekeeping);

        /// <summary>
        /// Delete Timekeeping with specific id
        /// </summary>
        /// <param name="Id">Timekeeping Id</param>
        void DeleteTimekeeping(int Id);

        /// <summary>
        /// Delete a Timekeeping with its Id and Update IsDeleted IF that Timekeeping has relationship with others
        /// </summary>
        /// <param name="Id">Id of Timekeeping</param>
        void DeleteTimekeepingRs(int Id);

        /// <summary>
        /// Update Timekeeping into database
        /// </summary>
        /// <param name="Timekeeping">Timekeeping object</param>
        void UpdateTimekeeping(Timekeeping Timekeeping);
    }
}
