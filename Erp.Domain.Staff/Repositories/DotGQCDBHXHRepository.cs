using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DotGQCDBHXHRepository : GenericRepository<ErpStaffDbContext, DotGQCDBHXH>, IDotGQCDBHXHRepository
    {
        public DotGQCDBHXHRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DotQGCDBHXH
        /// </summary>
        /// <returns>DotQGCDBHXH list</returns>
        public IQueryable<DotGQCDBHXH> GetAllDotGQCDBHXH()
        {
            return Context.DotGQCDBHXH.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get DotQGCDBHXH information by specific id
        /// </summary>
        /// <param name="DotGQCDBHXHId">Id of DotQGCDBHXH</param>
        /// <returns></returns>
        public DotGQCDBHXH GetDotGQCDBHXHById(int Id)
        {
            return Context.DotGQCDBHXH.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DotQGCDBHXH into database
        /// </summary>
        /// <param name="DotGQCDBHXH">Object infomation</param>
        public void InsertDotGQCDBHXH(DotGQCDBHXH DotGQCDBHXH)
        {
            Context.DotGQCDBHXH.Add(DotGQCDBHXH);
            Context.Entry(DotGQCDBHXH).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DotQGCDBHXH with specific id
        /// </summary>
        /// <param name="Id">DotQGCDBHXH Id</param>
        public void DeleteDotGQCDBHXH(int Id)
        {
            DotGQCDBHXH deletedDotQGCDBHXH = GetDotGQCDBHXHById(Id);
            Context.DotGQCDBHXH.Remove(deletedDotQGCDBHXH);
            Context.Entry(deletedDotQGCDBHXH).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DotQGCDBHXH with its Id and Update IsDeleted IF that DotQGCDBHXH has relationship with others
        /// </summary>
        /// <param name="DotQGCDBHXHId">Id of DotQGCDBHXH</param>
        public void DeleteDotGQCDBHXHRs(int Id)
        {
            DotGQCDBHXH deleteDotGQCDBHXHRs = GetDotGQCDBHXHById(Id);
            deleteDotGQCDBHXHRs.IsDeleted = true;
            UpdateDotGQCDBHXH(deleteDotGQCDBHXHRs);
        }

        /// <summary>
        /// Update DotQGCDBHXH into database
        /// </summary>
        /// <param name="DotGQCDBHXH">DotQGCDBHXH object</param>
        public void UpdateDotGQCDBHXH(DotGQCDBHXH DotGQCDBHXH)
        {
            Context.Entry(DotGQCDBHXH).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
