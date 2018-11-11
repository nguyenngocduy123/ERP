using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class WelfareProgramsDetailRepository : GenericRepository<ErpStaffDbContext, WelfareProgramsDetail>, IWelfareProgramsDetailRepository
    {
        public WelfareProgramsDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all WelfareProgramsDetail
        /// </summary>
        /// <returns>WelfareProgramsDetail list</returns>
        public IQueryable<WelfareProgramsDetail> GetAllWelfareProgramsDetail()
        {
            return Context.WelfareProgramsDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwWelfareProgramsDetail> GetAllvwWelfareProgramsDetail()
        {
            return Context.vwWelfareProgramsDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get WelfareProgramsDetail information by specific id
        /// </summary>
        /// <param name="WelfareProgramsDetailId">Id of WelfareProgramsDetail</param>
        /// <returns></returns>
        public WelfareProgramsDetail GetWelfareProgramsDetailById(int Id)
        {
            return Context.WelfareProgramsDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwWelfareProgramsDetail GetvwWelfareProgramsDetailById(int Id)
        {
            return Context.vwWelfareProgramsDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert WelfareProgramsDetail into database
        /// </summary>
        /// <param name="WelfareProgramsDetail">Object infomation</param>
        public void InsertWelfareProgramsDetail(WelfareProgramsDetail WelfareProgramsDetail)
        {
            Context.WelfareProgramsDetail.Add(WelfareProgramsDetail);
            Context.Entry(WelfareProgramsDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete WelfareProgramsDetail with specific id
        /// </summary>
        /// <param name="Id">WelfareProgramsDetail Id</param>
        public void DeleteWelfareProgramsDetail(int Id)
        {
            WelfareProgramsDetail deletedWelfareProgramsDetail = GetWelfareProgramsDetailById(Id);
            Context.WelfareProgramsDetail.Remove(deletedWelfareProgramsDetail);
            Context.Entry(deletedWelfareProgramsDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a WelfareProgramsDetail with its Id and Update IsDeleted IF that WelfareProgramsDetail has relationship with others
        /// </summary>
        /// <param name="WelfareProgramsDetailId">Id of WelfareProgramsDetail</param>
        public void DeleteWelfareProgramsDetailRs(int Id)
        {
            WelfareProgramsDetail deleteWelfareProgramsDetailRs = GetWelfareProgramsDetailById(Id);
            deleteWelfareProgramsDetailRs.IsDeleted = true;
            UpdateWelfareProgramsDetail(deleteWelfareProgramsDetailRs);
        }

        /// <summary>
        /// Update WelfareProgramsDetail into database
        /// </summary>
        /// <param name="WelfareProgramsDetail">WelfareProgramsDetail object</param>
        public void UpdateWelfareProgramsDetail(WelfareProgramsDetail WelfareProgramsDetail)
        {
            Context.Entry(WelfareProgramsDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
