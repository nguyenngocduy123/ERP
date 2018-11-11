using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class CommissionCusRepository : GenericRepository<ErpSaleDbContext, CommissionCus>, ICommissionCusRepository
    {
        public CommissionCusRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CommissionCus
        /// </summary>
        /// <returns>CommissionCus list</returns>
        public IQueryable<CommissionCus> GetAllCommissionCus()
        {
            return Context.CommissionCus.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get CommissionCus information by specific id
        /// </summary>
        /// <param name="CommissionCusId">Id of CommissionCus</param>
        /// <returns></returns>
        public CommissionCus GetCommissionCusById(int Id)
        {
            return Context.CommissionCus.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert CommissionCus into database
        /// </summary>
        /// <param name="CommissionCus">Object infomation</param>
        public void InsertCommissionCus(CommissionCus CommissionCus)
        {
            Context.CommissionCus.Add(CommissionCus);
            Context.Entry(CommissionCus).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CommissionCus with specific id
        /// </summary>
        /// <param name="Id">CommissionCus Id</param>
        public void DeleteCommissionCus(int Id)
        {
            CommissionCus deletedCommissionCus = GetCommissionCusById(Id);
            Context.CommissionCus.Remove(deletedCommissionCus);
            Context.Entry(deletedCommissionCus).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CommissionCus with its Id and Update IsDeleted IF that CommissionCus has relationship with others
        /// </summary>
        /// <param name="CommissionCusId">Id of CommissionCus</param>
        public void DeleteCommissionCusRs(int Id)
        {
            CommissionCus deleteCommissionCusRs = GetCommissionCusById(Id);
            deleteCommissionCusRs.IsDeleted = true;
            UpdateCommissionCus(deleteCommissionCusRs);
        }

        /// <summary>
        /// Update CommissionCus into database
        /// </summary>
        /// <param name="CommissionCus">CommissionCus object</param>
        public void UpdateCommissionCus(CommissionCus CommissionCus)
        {
            Context.Entry(CommissionCus).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
