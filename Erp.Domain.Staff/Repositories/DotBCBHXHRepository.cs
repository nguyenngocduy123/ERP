using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DotBCBHXHRepository : GenericRepository<ErpStaffDbContext, DotBCBHXH>, IDotBCBHXHRepository
    {
        public DotBCBHXHRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DotBCBHXH
        /// </summary>
        /// <returns>DotBCBHXH list</returns>
        public IQueryable<DotBCBHXH> GetAllDotBCBHXH()
        {
            return Context.DotBCBHXH.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get DotBCBHXH information by specific id
        /// </summary>
        /// <param name="DotBCBHXHId">Id of DotBCBHXH</param>
        /// <returns></returns>
        public DotBCBHXH GetDotBCBHXHById(int Id)
        {
            return Context.DotBCBHXH.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DotBCBHXH into database
        /// </summary>
        /// <param name="DotBCBHXH">Object infomation</param>
        public void InsertDotBCBHXH(DotBCBHXH DotBCBHXH)
        {
            Context.DotBCBHXH.Add(DotBCBHXH);
            Context.Entry(DotBCBHXH).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DotBCBHXH with specific id
        /// </summary>
        /// <param name="Id">DotBCBHXH Id</param>
        public void DeleteDotBCBHXH(int Id)
        {
            DotBCBHXH deletedDotBCBHXH = GetDotBCBHXHById(Id);
            Context.DotBCBHXH.Remove(deletedDotBCBHXH);
            Context.Entry(deletedDotBCBHXH).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DotBCBHXH with its Id and Update IsDeleted IF that DotBCBHXH has relationship with others
        /// </summary>
        /// <param name="DotBCBHXHId">Id of DotBCBHXH</param>
        public void DeleteDotBCBHXHRs(int Id)
        {
            DotBCBHXH deleteDotBCBHXHRs = GetDotBCBHXHById(Id);
            deleteDotBCBHXHRs.IsDeleted = true;
            UpdateDotBCBHXH(deleteDotBCBHXHRs);
        }

        /// <summary>
        /// Update DotBCBHXH into database
        /// </summary>
        /// <param name="DotBCBHXH">DotBCBHXH object</param>
        public void UpdateDotBCBHXH(DotBCBHXH DotBCBHXH)
        {
            Context.Entry(DotBCBHXH).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
