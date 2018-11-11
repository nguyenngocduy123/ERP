using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TaxIncomePersonDetailRepository : GenericRepository<ErpStaffDbContext, TaxIncomePersonDetail>, ITaxIncomePersonDetailRepository
    {
        public TaxIncomePersonDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TaxIncomePersonDetail
        /// </summary>
        /// <returns>TaxIncomePersonDetail list</returns>
        public IQueryable<TaxIncomePersonDetail> GetAllTaxIncomePersonDetail()
        {
            return Context.TaxIncomePersonDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwTaxIncomePersonDetail> GetAllvwTaxIncomePersonDetail()
        {
            return Context.vwTaxIncomePersonDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TaxIncomePersonDetail information by specific id
        /// </summary>
        /// <param name="TaxIncomePersonDetailId">Id of TaxIncomePersonDetail</param>
        /// <returns></returns>
        public TaxIncomePersonDetail GetTaxIncomePersonDetailById(int Id)
        {
            return Context.TaxIncomePersonDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert TaxIncomePersonDetail into database
        /// </summary>
        /// <param name="TaxIncomePersonDetail">Object infomation</param>
        public void InsertTaxIncomePersonDetail(TaxIncomePersonDetail TaxIncomePersonDetail)
        {
            Context.TaxIncomePersonDetail.Add(TaxIncomePersonDetail);
            Context.Entry(TaxIncomePersonDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TaxIncomePersonDetail with specific id
        /// </summary>
        /// <param name="Id">TaxIncomePersonDetail Id</param>
        public void DeleteTaxIncomePersonDetail(int Id)
        {
            TaxIncomePersonDetail deletedTaxIncomePersonDetail = GetTaxIncomePersonDetailById(Id);
            Context.TaxIncomePersonDetail.Remove(deletedTaxIncomePersonDetail);
            Context.Entry(deletedTaxIncomePersonDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TaxIncomePersonDetail with its Id and Update IsDeleted IF that TaxIncomePersonDetail has relationship with others
        /// </summary>
        /// <param name="TaxIncomePersonDetailId">Id of TaxIncomePersonDetail</param>
        public void DeleteTaxIncomePersonDetailRs(int Id)
        {
            TaxIncomePersonDetail deleteTaxIncomePersonDetailRs = GetTaxIncomePersonDetailById(Id);
            deleteTaxIncomePersonDetailRs.IsDeleted = true;
            UpdateTaxIncomePersonDetail(deleteTaxIncomePersonDetailRs);
        }

        /// <summary>
        /// Update TaxIncomePersonDetail into database
        /// </summary>
        /// <param name="TaxIncomePersonDetail">TaxIncomePersonDetail object</param>
        public void UpdateTaxIncomePersonDetail(TaxIncomePersonDetail TaxIncomePersonDetail)
        {
            Context.Entry(TaxIncomePersonDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
