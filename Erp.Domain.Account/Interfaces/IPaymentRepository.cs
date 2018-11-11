using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// Get all Payment
        /// </summary>
        /// <returns>Payment list</returns>
        IQueryable<Payment> GetAllPayment();
        IQueryable<vwPayment> GetAllvwPayment();
        IQueryable<vwPayment> GetAllvwPaymentFull();
        /// <summary>
        /// Get Payment information by specific id
        /// </summary>
        /// <param name="Id">Id of Payment</param>
        /// <returns></returns>
        Payment GetPaymentById(int Id);
        vwPayment GetvwPaymentById(int Id);
        vwPayment GetvwPaymentByTagetId(int TagetId,string TagetName, string Name);
        /// <summary>
        /// Insert Payment into database
        /// </summary>
        /// <param name="Payment">Object infomation</param>
        void InsertPayment(Payment Payment);

        /// <summary>
        /// Delete Payment with specific id
        /// </summary>
        /// <param name="Id">Payment Id</param>
        void DeletePayment(int Id);

        /// <summary>
        /// Delete a Payment with its Id and Update IsDeleted IF that Payment has relationship with others
        /// </summary>
        /// <param name="Id">Id of Payment</param>
        void DeletePaymentRs(int Id);

        /// <summary>
        /// Update Payment into database
        /// </summary>
        /// <param name="Payment">Payment object</param>
        void UpdatePayment(Payment Payment);
    }
}
