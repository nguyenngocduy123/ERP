using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ContractRepository : GenericRepository<ErpAccountDbContext, Contract>, IContractRepository
    {
        public ContractRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Contract
        /// </summary>
        /// <returns>Contract list</returns>
        public IQueryable<Contract> GetAllContract()
        {
            return Context.Contract.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwContract> GetAllvwContract()
        {
            return Context.vwContract.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwLogContractbyCondos> GetvwLogContractbyCondos(int? CondosId)
        {
            return Context.vwLogContractbyCondos.Where(item => item.CondosId==CondosId &&(item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Contract information by specific id
        /// </summary>
        /// <param name="ContractId">Id of Contract</param>
        /// <returns></returns>
        public Contract GetContractById(int? Id)
        {
            return Context.Contract.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwContract GetvwContractById(int Id)
        {
            return Context.vwContract.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public Contract GetContractByTransactionCode(string Id)
        {
            return Context.Contract.SingleOrDefault(item => item.TransactionCode == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Contract into database
        /// </summary>
        /// <param name="Contract">Object infomation</param>
        public void InsertContract(Contract Contract)
        {
            Context.Contract.Add(Contract);
            Context.Entry(Contract).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Contract with specific id
        /// </summary>
        /// <param name="Id">Contract Id</param>
        public void DeleteContract(int Id)
        {
            Contract deletedContract = GetContractById(Id);
            Context.Contract.Remove(deletedContract);
            Context.Entry(deletedContract).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Contract with its Id and Update IsDeleted IF that Contract has relationship with others
        /// </summary>
        /// <param name="ContractId">Id of Contract</param>
        public void DeleteContractRs(int Id)
        {
            Contract deleteContractRs = GetContractById(Id);
            deleteContractRs.IsDeleted = true;
            UpdateContract(deleteContractRs);
        }

        /// <summary>
        /// Update Contract into database
        /// </summary>
        /// <param name="Contract">Contract object</param>
        public void UpdateContract(Contract Contract)
        {
            Context.Entry(Contract).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
