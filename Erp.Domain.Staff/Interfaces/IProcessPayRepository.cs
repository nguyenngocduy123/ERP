using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IProcessPayRepository
    {
        /// <summary>
        /// Get all ProcessPay
        /// </summary>
        /// <returns>ProcessPay list</returns>
        IQueryable<ProcessPay> GetAllProcessPay();

        /// <summary>
        /// Get ProcessPay information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessPay</param>
        /// <returns></returns>
        ProcessPay GetProcessPayById(int Id);

        /// <summary>
        /// Insert ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">Object infomation</param>
        void InsertProcessPay(ProcessPay ProcessPay);

        /// <summary>
        /// Delete ProcessPay with specific id
        /// </summary>
        /// <param name="Id">ProcessPay Id</param>
        void DeleteProcessPay(int Id);

        /// <summary>
        /// Delete a ProcessPay with its Id and Update IsDeleted IF that ProcessPay has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessPay</param>
        void DeleteProcessPayRs(int Id);

        /// <summary>
        /// Update ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">ProcessPay object</param>
        void UpdateProcessPay(ProcessPay ProcessPay);
    }
}
