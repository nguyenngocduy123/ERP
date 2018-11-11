using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ICommisionStaffRepository
    {
        /// <summary>
        /// Get all CommisionStaff
        /// </summary>
        /// <returns>CommisionStaff list</returns>
        IQueryable<CommisionStaff> GetAllCommisionStaff();
        IQueryable<vwCommisionStaff> GetAllvwCommisionStaff();
        /// <summary>
        /// Get CommisionStaff information by specific id
        /// </summary>
        /// <param name="Id">Id of CommisionStaff</param>
        /// <returns></returns>
        CommisionStaff GetCommisionStaffById(int Id);

        /// <summary>
        /// Insert CommisionStaff into database
        /// </summary>
        /// <param name="CommisionStaff">Object infomation</param>
        void InsertCommisionStaff(CommisionStaff CommisionStaff);

        /// <summary>
        /// Delete CommisionStaff with specific id
        /// </summary>
        /// <param name="Id">CommisionStaff Id</param>
        void DeleteCommisionStaff(int Id);

        /// <summary>
        /// Delete a CommisionStaff with its Id and Update IsDeleted IF that CommisionStaff has relationship with others
        /// </summary>
        /// <param name="Id">Id of CommisionStaff</param>
        void DeleteCommisionStaffRs(int Id);

        /// <summary>
        /// Update CommisionStaff into database
        /// </summary>
        /// <param name="CommisionStaff">CommisionStaff object</param>
        void UpdateCommisionStaff(CommisionStaff CommisionStaff);
    }
}
