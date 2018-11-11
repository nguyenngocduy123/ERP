using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ContractSellRepository : GenericRepository<ErpAccountDbContext, ContractSell>, IContractSellRepository
    {
        public ContractSellRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ContractSell
        /// </summary>
        /// <returns>ContractSell list</returns>
        public IQueryable<ContractSell> GetAllContractSell()
        {
            return Context.ContractSell.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwContractSell> GetAllvwContractSell()
        {
            return Context.vwContractSell.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get ContractSell information by specific id
        /// </summary>
        /// <param name="ContractSellId">Id of ContractSell</param>
        /// <returns></returns>
        public ContractSell GetContractSellById(int Id)
        {
            return Context.ContractSell.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwContractSell GetvwContractSellById(int Id)
        {
            return Context.vwContractSell.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert ContractSell into database
        /// </summary>
        /// <param name="ContractSell">Object infomation</param>
        public void InsertContractSell(ContractSell ContractSell)
        {
            Context.ContractSell.Add(ContractSell);
            Context.Entry(ContractSell).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ContractSell with specific id
        /// </summary>
        /// <param name="Id">ContractSell Id</param>
        public void DeleteContractSell(int Id)
        {
            ContractSell deletedContractSell = GetContractSellById(Id);
            Context.ContractSell.Remove(deletedContractSell);
            Context.Entry(deletedContractSell).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ContractSell with its Id and Update IsDeleted IF that ContractSell has relationship with others
        /// </summary>
        /// <param name="ContractSellId">Id of ContractSell</param>
        public void DeleteContractSellRs(int Id)
        {
            ContractSell deleteContractSellRs = GetContractSellById(Id);
            deleteContractSellRs.IsDeleted = true;
            UpdateContractSell(deleteContractSellRs);
        }

        /// <summary>
        /// Update ContractSell into database
        /// </summary>
        /// <param name="ContractSell">ContractSell object</param>
        public void UpdateContractSell(ContractSell ContractSell)
        {
            Context.Entry(ContractSell).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
