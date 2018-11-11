using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ContractLeaseRepository : GenericRepository<ErpAccountDbContext, ContractLease>, IContractLeaseRepository
    {
        public ContractLeaseRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ContractLease
        /// </summary>
        /// <returns>ContractLease list</returns>
        public IQueryable<ContractLease> GetAllContractLease()
        {
            return Context.ContractLease.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwContractLease> GetAllvwContractLease()
        {
            return Context.vwContractLease.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get ContractLease information by specific id
        /// </summary>
        /// <param name="ContractLeaseId">Id of ContractLease</param>
        /// <returns></returns>
        public ContractLease GetContractLeaseById(int Id)
        {
            return Context.ContractLease.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwContractLease GetvwContractLeaseById(int Id)
        {
            return Context.vwContractLease.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ContractLease into database
        /// </summary>
        /// <param name="ContractLease">Object infomation</param>
        public void InsertContractLease(ContractLease ContractLease)
        {
            Context.ContractLease.Add(ContractLease);
            Context.Entry(ContractLease).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ContractLease with specific id
        /// </summary>
        /// <param name="Id">ContractLease Id</param>
        public void DeleteContractLease(int Id)
        {
            ContractLease deletedContractLease = GetContractLeaseById(Id);
            Context.ContractLease.Remove(deletedContractLease);
            Context.Entry(deletedContractLease).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ContractLease with its Id and Update IsDeleted IF that ContractLease has relationship with others
        /// </summary>
        /// <param name="ContractLeaseId">Id of ContractLease</param>
        public void DeleteContractLeaseRs(int Id)
        {
            ContractLease deleteContractLeaseRs = GetContractLeaseById(Id);
            deleteContractLeaseRs.IsDeleted = true;
            UpdateContractLease(deleteContractLeaseRs);
        }

        /// <summary>
        /// Update ContractLease into database
        /// </summary>
        /// <param name="ContractLease">ContractLease object</param>
        public void UpdateContractLease(ContractLease ContractLease)
        {
            Context.Entry(ContractLease).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
