using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class TotalDiscountMoneyNTRepository : GenericRepository<ErpSaleDbContext, TotalDiscountMoneyNT>, ITotalDiscountMoneyNTRepository
    {
        public TotalDiscountMoneyNTRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TotalDiscountMoneyNT
        /// </summary>
        /// <returns>TotalDiscountMoneyNT list</returns>
        public IQueryable<TotalDiscountMoneyNT> GetAllTotalDiscountMoneyNT()
        {
            return Context.TotalDiscountMoneyNT.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTotalDiscountMoneyNT> GetvwAllTotalDiscountMoneyNT()
        {
            return Context.vwTotalDiscountMoneyNT.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TotalDiscountMoneyNT information by specific id
        /// </summary>
        /// <param name="TotalDiscountMoneyNTId">Id of TotalDiscountMoneyNT</param>
        /// <returns></returns>
        public TotalDiscountMoneyNT GetTotalDiscountMoneyNTById(int Id)
        {
            return Context.TotalDiscountMoneyNT.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwTotalDiscountMoneyNT GetvwTotalDiscountMoneyNTById(int Id)
        {
            return Context.vwTotalDiscountMoneyNT.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert TotalDiscountMoneyNT into database
        /// </summary>
        /// <param name="TotalDiscountMoneyNT">Object infomation</param>
        public void InsertTotalDiscountMoneyNT(TotalDiscountMoneyNT TotalDiscountMoneyNT)
        {
            Context.TotalDiscountMoneyNT.Add(TotalDiscountMoneyNT);
            Context.Entry(TotalDiscountMoneyNT).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TotalDiscountMoneyNT with specific id
        /// </summary>
        /// <param name="Id">TotalDiscountMoneyNT Id</param>
        public void DeleteTotalDiscountMoneyNT(int Id)
        {
            TotalDiscountMoneyNT deletedTotalDiscountMoneyNT = GetTotalDiscountMoneyNTById(Id);
            Context.TotalDiscountMoneyNT.Remove(deletedTotalDiscountMoneyNT);
            Context.Entry(deletedTotalDiscountMoneyNT).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TotalDiscountMoneyNT with its Id and Update IsDeleted IF that TotalDiscountMoneyNT has relationship with others
        /// </summary>
        /// <param name="TotalDiscountMoneyNTId">Id of TotalDiscountMoneyNT</param>
        public void DeleteTotalDiscountMoneyNTRs(int Id)
        {
            TotalDiscountMoneyNT deleteTotalDiscountMoneyNTRs = GetTotalDiscountMoneyNTById(Id);
            deleteTotalDiscountMoneyNTRs.IsDeleted = true;
            UpdateTotalDiscountMoneyNT(deleteTotalDiscountMoneyNTRs);
        }

        /// <summary>
        /// Update TotalDiscountMoneyNT into database
        /// </summary>
        /// <param name="TotalDiscountMoneyNT">TotalDiscountMoneyNT object</param>
        public void UpdateTotalDiscountMoneyNT(TotalDiscountMoneyNT TotalDiscountMoneyNT)
        {
            Context.Entry(TotalDiscountMoneyNT).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
