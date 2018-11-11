using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IProcessPaymentRepository
    {
        /// <summary>
        /// Get all ProcessPayment
        /// </summary>
        /// <returns>ProcessPayment list</returns>
        IQueryable<vwProcessPayment> GetAllProcessPayment();

        /// <summary>
        /// Get ProcessPayment information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessPayment</param>
        /// <returns></returns>
        ProcessPayment GetProcessPaymentById(int Id);

        /// <summary>
        /// Insert ProcessPayment into database
        /// </summary>
        /// <param name="ProcessPayment">Object infomation</param>
        void InsertProcessPayment(ProcessPayment ProcessPayment);

        /// <summary>
        /// Delete ProcessPayment with specific id
        /// </summary>
        /// <param name="Id">ProcessPayment Id</param>
        void DeleteProcessPayment(int Id);

        /// <summary>
        /// Delete a ProcessPayment with its Id and Update IsDeleted IF that ProcessPayment has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessPayment</param>
        void DeleteProcessPaymentRs(int Id);

        /// <summary>
        /// Update ProcessPayment into database
        /// </summary>
        /// <param name="ProcessPayment">ProcessPayment object</param>
        void UpdateProcessPayment(ProcessPayment ProcessPayment);
    }
}
