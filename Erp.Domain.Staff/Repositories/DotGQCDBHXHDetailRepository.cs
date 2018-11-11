using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DotGQCDBHXHDetailRepository : GenericRepository<ErpStaffDbContext, DotGQCDBHXHDetail>, IDotGQCDBHXHDetailRepository
    {
        public DotGQCDBHXHDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DotGQCDBHXHDetail
        /// </summary>
        /// <returns>DotGQCDBHXHDetail list</returns>
        public IQueryable<DotGQCDBHXHDetail> GetAllDotGQCDBHXHDetail()
        {
            return Context.DotGQCDBHXHDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwDotGQCDBHXHDetail> GetAllvwDotGQCDBHXHDetail()
        {
            return Context.vwDotGQCDBHXHDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get DotGQCDBHXHDetail information by specific id
        /// </summary>
        /// <param name="DotGQCDBHXHDetailId">Id of DotGQCDBHXHDetail</param>
        /// <returns></returns>
        public DotGQCDBHXHDetail GetDotGQCDBHXHDetailById(int Id)
        {
            return Context.DotGQCDBHXHDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwDotGQCDBHXHDetail GetvwDotGQCDBHXHDetailById(int Id)
        {
            return Context.vwDotGQCDBHXHDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert DotGQCDBHXHDetail into database
        /// </summary>
        /// <param name="DotGQCDBHXHDetail">Object infomation</param>
        public void InsertDotGQCDBHXHDetail(DotGQCDBHXHDetail DotGQCDBHXHDetail)
        {
            Context.DotGQCDBHXHDetail.Add(DotGQCDBHXHDetail);
            Context.Entry(DotGQCDBHXHDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DotGQCDBHXHDetail with specific id
        /// </summary>
        /// <param name="Id">DotGQCDBHXHDetail Id</param>
        public void DeleteDotGQCDBHXHDetail(int Id)
        {
            DotGQCDBHXHDetail deletedDotGQCDBHXHDetail = GetDotGQCDBHXHDetailById(Id);
            Context.DotGQCDBHXHDetail.Remove(deletedDotGQCDBHXHDetail);
            Context.Entry(deletedDotGQCDBHXHDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DotGQCDBHXHDetail with its Id and Update IsDeleted IF that DotGQCDBHXHDetail has relationship with others
        /// </summary>
        /// <param name="DotGQCDBHXHDetailId">Id of DotGQCDBHXHDetail</param>
        public void DeleteDotGQCDBHXHDetailRs(int Id)
        {
            DotGQCDBHXHDetail deleteDotGQCDBHXHDetailRs = GetDotGQCDBHXHDetailById(Id);
            deleteDotGQCDBHXHDetailRs.IsDeleted = true;
            UpdateDotGQCDBHXHDetail(deleteDotGQCDBHXHDetailRs);
        }

        /// <summary>
        /// Update DotGQCDBHXHDetail into database
        /// </summary>
        /// <param name="DotGQCDBHXHDetail">DotGQCDBHXHDetail object</param>
        public void UpdateDotGQCDBHXHDetail(DotGQCDBHXHDetail DotGQCDBHXHDetail)
        {
            Context.Entry(DotGQCDBHXHDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
