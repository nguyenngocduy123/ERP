using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class UsingServiceLogDetailRepository : GenericRepository<ErpSaleDbContext, UsingServiceLogDetail>, IUsingServiceLogDetailRepository
    {
        public UsingServiceLogDetailRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all UsingServiceLogDetail
        /// </summary>
        /// <returns>UsingServiceLogDetail list</returns>
        public IQueryable<UsingServiceLogDetail> GetAllUsingServiceLogDetail()
        {
            return Context.UsingServiceLogDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwUsingServiceLogDetail> GetAllvwUsingServiceLogDetail()
        {
            return Context.vwUsingServiceLogDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get UsingServiceLogDetail information by specific id
        /// </summary>
        /// <param name="UsingServiceLogDetailId">Id of UsingServiceLogDetail</param>
        /// <returns></returns>
        public UsingServiceLogDetail GetUsingServiceLogDetailById(int Id)
        {
            return Context.UsingServiceLogDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwUsingServiceLogDetail GetvwUsingServiceLogDetailById(int Id)
        {
            return Context.vwUsingServiceLogDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert UsingServiceLogDetail into database
        /// </summary>
        /// <param name="UsingServiceLogDetail">Object infomation</param>
        public void InsertUsingServiceLogDetail(UsingServiceLogDetail UsingServiceLogDetail)
        {
            Context.UsingServiceLogDetail.Add(UsingServiceLogDetail);
            Context.Entry(UsingServiceLogDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete UsingServiceLogDetail with specific id
        /// </summary>
        /// <param name="Id">UsingServiceLogDetail Id</param>
        public void DeleteUsingServiceLogDetail(int Id)
        {
            UsingServiceLogDetail deletedUsingServiceLogDetail = GetUsingServiceLogDetailById(Id);
            Context.UsingServiceLogDetail.Remove(deletedUsingServiceLogDetail);
            Context.Entry(deletedUsingServiceLogDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a UsingServiceLogDetail with its Id and Update IsDeleted IF that UsingServiceLogDetail has relationship with others
        /// </summary>
        /// <param name="UsingServiceLogDetailId">Id of UsingServiceLogDetail</param>
        public void DeleteUsingServiceLogDetailRs(int Id)
        {
            UsingServiceLogDetail deleteUsingServiceLogDetailRs = GetUsingServiceLogDetailById(Id);
            deleteUsingServiceLogDetailRs.IsDeleted = true;
            UpdateUsingServiceLogDetail(deleteUsingServiceLogDetailRs);
        }

        /// <summary>
        /// Update UsingServiceLogDetail into database
        /// </summary>
        /// <param name="UsingServiceLogDetail">UsingServiceLogDetail object</param>
        public void UpdateUsingServiceLogDetail(UsingServiceLogDetail UsingServiceLogDetail)
        {
            Context.Entry(UsingServiceLogDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
