using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class CustomerRepository : GenericRepository<ErpAccountDbContext, Customer>, ICustomerRepository
    {
        public CustomerRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Customer
        /// </summary>
        /// <returns>Customer list</returns>
        public IQueryable<Customer> GetAllCustomer()
        {
            return Context.Customer.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwCustomer> GetAllvwCustomer()
        {
            return Context.vwCustomer.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Customer information by specific id
        /// </summary>
        /// <param name="CustomerId">Id of Customer</param>
        /// <returns></returns>
        public Customer GetCustomerById(int Id)
        {
            return Context.Customer.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public Customer GetCustomerByPhone(string Phone)
        {
            return Context.Customer.SingleOrDefault(item => item.Phone == Phone && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwCustomer GetvwCustomerById(int Id)
        {
            return Context.vwCustomer.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwCustomer GetvwCustomerByCode(string Code)
        {
            return Context.vwCustomer.SingleOrDefault(item => item.Code == Code && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public Customer GetCustomerByGuestsCustomer(int? BranchId)
        {
            return Context.Customer.SingleOrDefault(item => item.CustomerType == "Guests"&&item.BranchId==BranchId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Customer into database
        /// </summary>
        /// <param name="Customer">Object infomation</param>
        public void InsertCustomer(Customer Customer)
        {
            Context.Customer.Add(Customer);
            Context.Entry(Customer).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Customer with specific id
        /// </summary>
        /// <param name="Id">Customer Id</param>
        public void DeleteCustomer(int Id)
        {
            Customer deletedCustomer = GetCustomerById(Id);
            Context.Customer.Remove(deletedCustomer);
            Context.Entry(deletedCustomer).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Customer with its Id and Update IsDeleted IF that Customer has relationship with others
        /// </summary>
        /// <param name="CustomerId">Id of Customer</param>
        public void DeleteCustomerRs(int Id)
        {
            Customer deleteCustomerRs = GetCustomerById(Id);
            deleteCustomerRs.IsDeleted = true;
            UpdateCustomer(deleteCustomerRs);
        }

        /// <summary>
        /// Update Customer into database
        /// </summary>
        /// <param name="Customer">Customer object</param>
        public void UpdateCustomer(Customer Customer)
        {
            Context.Entry(Customer).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
