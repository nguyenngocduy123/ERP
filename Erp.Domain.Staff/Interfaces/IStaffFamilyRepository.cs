using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IStaffFamilyRepository
    {
        /// <summary>
        /// Get all StaffFamily
        /// </summary>
        /// <returns>StaffFamily list</returns>
        IQueryable<StaffFamily> GetAllStaffFamily();

        /// <summary>
        /// Get StaffFamily information by specific id
        /// </summary>
        /// <param name="Id">Id of StaffFamily</param>
        /// <returns></returns>
        StaffFamily GetStaffFamilyById(int Id);

        /// <summary>
        /// Insert StaffFamily into database
        /// </summary>
        /// <param name="StaffFamily">Object infomation</param>
        void InsertStaffFamily(StaffFamily StaffFamily);

        /// <summary>
        /// Delete StaffFamily with specific id
        /// </summary>
        /// <param name="Id">StaffFamily Id</param>
        void DeleteStaffFamily(int Id);

        /// <summary>
        /// Delete a StaffFamily with its Id and Update IsDeleted IF that StaffFamily has relationship with others
        /// </summary>
        /// <param name="Id">Id of StaffFamily</param>
        void DeleteStaffFamilyRs(int Id);

        /// <summary>
        /// Update StaffFamily into database
        /// </summary>
        /// <param name="StaffFamily">StaffFamily object</param>
        void UpdateStaffFamily(StaffFamily StaffFamily);
        IQueryable<StaffFamily> GetAllStaffFamilyByStaffId(int staffId);
    }
}
