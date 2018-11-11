using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITimekeepingListRepository
    {
        /// <summary>
        /// Get all TimekeepingList
        /// </summary>
        /// <returns>TimekeepingList list</returns>
        IQueryable<TimekeepingList> GetAllTimekeepingList();
        IQueryable<vwTimekeepingList> GetAllvwTimekeepingList();
        /// <summary>
        /// Get TimekeepingList information by specific id
        /// </summary>
        /// <param name="Id">Id of TimekeepingList</param>
        /// <returns></returns>
        TimekeepingList GetTimekeepingListById(int Id);
        vwTimekeepingList GetvwTimekeepingListById(int Id);
        /// <summary>
        /// Insert TimekeepingList into database
        /// </summary>
        /// <param name="TimekeepingList">Object infomation</param>
        void InsertTimekeepingList(TimekeepingList TimekeepingList);

        /// <summary>
        /// Delete TimekeepingList with specific id
        /// </summary>
        /// <param name="Id">TimekeepingList Id</param>
        void DeleteTimekeepingList(int Id);

        /// <summary>
        /// Delete a TimekeepingList with its Id and Update IsDeleted IF that TimekeepingList has relationship with others
        /// </summary>
        /// <param name="Id">Id of TimekeepingList</param>
        void DeleteTimekeepingListRs(int Id);

        /// <summary>
        /// Update TimekeepingList into database
        /// </summary>
        /// <param name="TimekeepingList">TimekeepingList object</param>
        void UpdateTimekeepingList(TimekeepingList TimekeepingList);
    }
}
