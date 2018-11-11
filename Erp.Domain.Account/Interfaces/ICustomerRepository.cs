using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get all Customer
        /// </summary>
        /// <returns>Customer list</returns>
        IQueryable<Customer> GetAllCustomer();
        IQueryable<vwCustomer> GetAllvwCustomer();
        /// <summary>
        /// Get Customer information by specific id
        /// </summary>
        /// <param name="Id">Id of Customer</param>
        /// <returns></returns>
        Customer GetCustomerById(int Id);
        vwCustomer GetvwCustomerById(int Id);
        vwCustomer GetvwCustomerByCode(string Code);
        Customer GetCustomerByPhone(string Phone);
        Customer GetCustomerByGuestsCustomer(int? BranchId);
        /// <summary>
        /// Insert Customer into database
        /// </summary>
        /// <param name="Customer">Object infomation</param>
        void InsertCustomer(Customer Customer);

        /// <summary>
        /// Delete Customer with specific id
        /// </summary>
        /// <param name="Id">Customer Id</param>
        void DeleteCustomer(int Id);

        /// <summary>
        /// Delete a Customer with its Id and Update IsDeleted IF that Customer has relationship with others
        /// </summary>
        /// <param name="Id">Id of Customer</param>
        void DeleteCustomerRs(int Id);

        /// <summary>
        /// Update Customer into database
        /// </summary>
        /// <param name="Customer">Customer object</param>
        void UpdateCustomer(Customer Customer);
    }
}
