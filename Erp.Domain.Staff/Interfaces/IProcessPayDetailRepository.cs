using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IProcessPayDetailRepository
    {
        /// <summary>
        /// Get all ProcessPay
        /// </summary>
        /// <returns>ProcessPay list</returns>
        IQueryable<ProcessPayDetail> GetAllProcessPayDetail();

        /// <summary>
        /// Get ProcessPay information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessPay</param>
        /// <returns></returns>
        ProcessPayDetail GetProcessPayDetailById(int Id);

        /// <summary>
        /// Insert ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">Object infomation</param>
        void InsertProcessPayDetail(ProcessPayDetail ProcessPayDetail);

        /// <summary>
        /// Delete ProcessPay with specific id
        /// </summary>
        /// <param name="Id">ProcessPay Id</param>
        void DeleteProcessPayDetail(int Id);

        /// <summary>
        /// Delete a ProcessPay with its Id and Update IsDeleted IF that ProcessPay has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessPay</param>
        void DeleteProcessPayDetailRs(int Id);

        /// <summary>
        /// Update ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">ProcessPay object</param>
        void UpdateProcessPayDetail(ProcessPayDetail ProcessPayDetail);
    }
}
