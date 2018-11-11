using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class CustomerDiscountRepository : GenericRepository<ErpAccountDbContext, CustomerDiscount>, ICustomerDiscountRepository
    {
        public CustomerDiscountRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CustomerDiscount
        /// </summary>
        /// <returns>CustomerDiscount list</returns>
        public IQueryable<CustomerDiscount> GetAllCustomerDiscount()
        {
            return Context.CustomerDiscount.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get CustomerDiscount information by specific id
        /// </summary>
        /// <param name="CustomerDiscountId">Id of CustomerDiscount</param>
        /// <returns></returns>
        public CustomerDiscount GetCustomerDiscountById(int Id)
        {
            return Context.CustomerDiscount.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert CustomerDiscount into database
        /// </summary>
        /// <param name="CustomerDiscount">Object infomation</param>
        public void InsertCustomerDiscount(CustomerDiscount CustomerDiscount)
        {
            Context.CustomerDiscount.Add(CustomerDiscount);
            Context.Entry(CustomerDiscount).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CustomerDiscount with specific id
        /// </summary>
        /// <param name="Id">CustomerDiscount Id</param>
        public void DeleteCustomerDiscount(int Id)
        {
            CustomerDiscount deletedCustomerDiscount = GetCustomerDiscountById(Id);
            Context.CustomerDiscount.Remove(deletedCustomerDiscount);
            Context.Entry(deletedCustomerDiscount).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CustomerDiscount with its Id and Update IsDeleted IF that CustomerDiscount has relationship with others
        /// </summary>
        /// <param name="CustomerDiscountId">Id of CustomerDiscount</param>
        public void DeleteCustomerDiscountRs(int Id)
        {
            CustomerDiscount deleteCustomerDiscountRs = GetCustomerDiscountById(Id);
            deleteCustomerDiscountRs.IsDeleted = true;
            UpdateCustomerDiscount(deleteCustomerDiscountRs);
        }

        /// <summary>
        /// Update CustomerDiscount into database
        /// </summary>
        /// <param name="CustomerDiscount">CustomerDiscount object</param>
        public void UpdateCustomerDiscount(CustomerDiscount CustomerDiscount)
        {
            Context.Entry(CustomerDiscount).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
