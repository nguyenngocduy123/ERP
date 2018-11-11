using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface ICustomerDiscountRepository
    {
        /// <summary>
        /// Get all CustomerDiscount
        /// </summary>
        /// <returns>CustomerDiscount list</returns>
        IQueryable<CustomerDiscount> GetAllCustomerDiscount();

        /// <summary>
        /// Get CustomerDiscount information by specific id
        /// </summary>
        /// <param name="Id">Id of CustomerDiscount</param>
        /// <returns></returns>
        CustomerDiscount GetCustomerDiscountById(int Id);

        /// <summary>
        /// Insert CustomerDiscount into database
        /// </summary>
        /// <param name="CustomerDiscount">Object infomation</param>
        void InsertCustomerDiscount(CustomerDiscount CustomerDiscount);

        /// <summary>
        /// Delete CustomerDiscount with specific id
        /// </summary>
        /// <param name="Id">CustomerDiscount Id</param>
        void DeleteCustomerDiscount(int Id);

        /// <summary>
        /// Delete a CustomerDiscount with its Id and Update IsDeleted IF that CustomerDiscount has relationship with others
        /// </summary>
        /// <param name="Id">Id of CustomerDiscount</param>
        void DeleteCustomerDiscountRs(int Id);

        /// <summary>
        /// Update CustomerDiscount into database
        /// </summary>
        /// <param name="CustomerDiscount">CustomerDiscount object</param>
        void UpdateCustomerDiscount(CustomerDiscount CustomerDiscount);
    }
}
