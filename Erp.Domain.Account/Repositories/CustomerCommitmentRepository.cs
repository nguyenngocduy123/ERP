using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class CustomerCommitmentRepository : GenericRepository<ErpAccountDbContext, CustomerCommitment>, ICustomerCommitmentRepository
    {
        public CustomerCommitmentRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        
        public IQueryable<CustomerCommitment> GetAllCustomerCommitment()
        {
            return Context.CustomerCommitment.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public CustomerCommitment GetCustomerCommitmentById(int Id)
        {
            return Context.CustomerCommitment.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertCustomerCommitment(CustomerCommitment CustomerCommitment)
        {
            Context.CustomerCommitment.Add(CustomerCommitment);
            Context.Entry(CustomerCommitment).State = EntityState.Added;
            Context.SaveChanges();
        }

        
        public void DeleteCustomerCommitment(int Id)
        {
            CustomerCommitment deletedCustomerCommitment = GetCustomerCommitmentById(Id);
            Context.CustomerCommitment.Remove(deletedCustomerCommitment);
            Context.Entry(deletedCustomerCommitment).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        
        public void DeleteCustomerCommitmentRs(int Id)
        {
            CustomerCommitment deleteCustomerCommitmentRs = GetCustomerCommitmentById(Id);
            deleteCustomerCommitmentRs.IsDeleted = true;
            UpdateCustomerCommitment(deleteCustomerCommitmentRs);
        }

        
        public void UpdateCustomerCommitment(CustomerCommitment CustomerCommitment)
        {
            Context.Entry(CustomerCommitment).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
