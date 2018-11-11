using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DotBCBHXHDetailRepository : GenericRepository<ErpStaffDbContext, DotBCBHXHDetail>, IDotBCBHXHDetailRepository
    {
        public DotBCBHXHDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DotBCBHXHDetail
        /// </summary>
        /// <returns>DotBCBHXHDetail list</returns>
        public IQueryable<DotBCBHXHDetail> GetAllDotBCBHXHDetail()
        {
            return Context.DotBCBHXHDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwDotBCBHXHDetail> GetAllvwDotBCBHXHDetailByDotBCBHXHId(int DotBCBHXHId)
        {
            return Context.vwDotBCBHXHDetail.Where(item => item.DotBCBHXHId == DotBCBHXHId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwDotBCBHXHDetail> GetAllViewDotBCBHXHDetail()
        {
            return Context.vwDotBCBHXHDetail.Where(item =>  (item.IsDeleted == null || item.IsDeleted == false));
        }
    
        /// <summary>
        /// Get DotBCBHXHDetail information by specific id
        /// </summary>
        /// <param name="DotBCBHXHDetailId">Id of DotBCBHXHDetail</param>
        /// <returns></returns>
        public DotBCBHXHDetail GetDotBCBHXHDetailById(int Id)
        {
            return Context.DotBCBHXHDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DotBCBHXHDetail into database
        /// </summary>
        /// <param name="DotBCBHXHDetail">Object infomation</param>
        public void InsertDotBCBHXHDetail(DotBCBHXHDetail DotBCBHXHDetail)
        {
            Context.DotBCBHXHDetail.Add(DotBCBHXHDetail);
            Context.Entry(DotBCBHXHDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DotBCBHXHDetail with specific id
        /// </summary>
        /// <param name="Id">DotBCBHXHDetail Id</param>
        public void DeleteDotBCBHXHDetail(int Id)
        {
            DotBCBHXHDetail deletedDotBCBHXHDetail = GetDotBCBHXHDetailById(Id);
            Context.DotBCBHXHDetail.Remove(deletedDotBCBHXHDetail);
            Context.Entry(deletedDotBCBHXHDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DotBCBHXHDetail with its Id and Update IsDeleted IF that DotBCBHXHDetail has relationship with others
        /// </summary>
        /// <param name="DotBCBHXHDetailId">Id of DotBCBHXHDetail</param>
        public void DeleteDotBCBHXHDetailRs(int Id)
        {
            DotBCBHXHDetail deleteDotBCBHXHDetailRs = GetDotBCBHXHDetailById(Id);
            deleteDotBCBHXHDetailRs.IsDeleted = true;
            UpdateDotBCBHXHDetail(deleteDotBCBHXHDetailRs);
        }

        /// <summary>
        /// Update DotBCBHXHDetail into database
        /// </summary>
        /// <param name="DotBCBHXHDetail">DotBCBHXHDetail object</param>
        public void UpdateDotBCBHXHDetail(DotBCBHXHDetail DotBCBHXHDetail)
        {
            Context.Entry(DotBCBHXHDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
