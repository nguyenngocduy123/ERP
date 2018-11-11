using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class CommisionStaffRepository : GenericRepository<ErpSaleDbContext, CommisionStaff>, ICommisionStaffRepository
    {
        public CommisionStaffRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CommisionStaff
        /// </summary>
        /// <returns>CommisionStaff list</returns>
        public IQueryable<CommisionStaff> GetAllCommisionStaff()
        {
            return Context.CommisionStaff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwCommisionStaff> GetAllvwCommisionStaff()
        {
            return Context.vwCommisionStaff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get CommisionStaff information by specific id
        /// </summary>
        /// <param name="CommisionStaffId">Id of CommisionStaff</param>
        /// <returns></returns>
        public CommisionStaff GetCommisionStaffById(int Id)
        {
            return Context.CommisionStaff.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert CommisionStaff into database
        /// </summary>
        /// <param name="CommisionStaff">Object infomation</param>
        public void InsertCommisionStaff(CommisionStaff CommisionStaff)
        {
            Context.CommisionStaff.Add(CommisionStaff);
            Context.Entry(CommisionStaff).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CommisionStaff with specific id
        /// </summary>
        /// <param name="Id">CommisionStaff Id</param>
        public void DeleteCommisionStaff(int Id)
        {
            CommisionStaff deletedCommisionStaff = GetCommisionStaffById(Id);
            Context.CommisionStaff.Remove(deletedCommisionStaff);
            Context.Entry(deletedCommisionStaff).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CommisionStaff with its Id and Update IsDeleted IF that CommisionStaff has relationship with others
        /// </summary>
        /// <param name="CommisionStaffId">Id of CommisionStaff</param>
        public void DeleteCommisionStaffRs(int Id)
        {
            CommisionStaff deleteCommisionStaffRs = GetCommisionStaffById(Id);
            deleteCommisionStaffRs.IsDeleted = true;
            UpdateCommisionStaff(deleteCommisionStaffRs);
        }

        /// <summary>
        /// Update CommisionStaff into database
        /// </summary>
        /// <param name="CommisionStaff">CommisionStaff object</param>
        public void UpdateCommisionStaff(CommisionStaff CommisionStaff)
        {
            Context.Entry(CommisionStaff).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
