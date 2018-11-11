using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalarySettingDetailRepository : GenericRepository<ErpStaffDbContext, SalarySettingDetail>, ISalarySettingDetailRepository
    {
        public SalarySettingDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalarySettingDetail
        /// </summary>
        /// <returns>SalarySettingDetail list</returns>
        public IQueryable<SalarySettingDetail> GetAllSalarySettingDetail()
        {
            return Context.SalarySettingDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SalarySettingDetail information by specific id
        /// </summary>
        /// <param name="SalarySettingDetailId">Id of SalarySettingDetail</param>
        /// <returns></returns>
        public SalarySettingDetail GetSalarySettingDetailById(int Id)
        {
            return Context.SalarySettingDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert SalarySettingDetail into database
        /// </summary>
        /// <param name="SalarySettingDetail">Object infomation</param>
        public void InsertSalarySettingDetail(SalarySettingDetail SalarySettingDetail)
        {
            Context.SalarySettingDetail.Add(SalarySettingDetail);
            Context.Entry(SalarySettingDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalarySettingDetail with specific id
        /// </summary>
        /// <param name="Id">SalarySettingDetail Id</param>
        public void DeleteSalarySettingDetail(int Id)
        {
            SalarySettingDetail deletedSalarySettingDetail = GetSalarySettingDetailById(Id);
            Context.SalarySettingDetail.Remove(deletedSalarySettingDetail);
            Context.Entry(deletedSalarySettingDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a SalarySettingDetail with its Id and Update IsDeleted IF that SalarySettingDetail has relationship with others
        /// </summary>
        /// <param name="SalarySettingDetailId">Id of SalarySettingDetail</param>
        public void DeleteSalarySettingDetailRs(int Id)
        {
            SalarySettingDetail deleteSalarySettingDetailRs = GetSalarySettingDetailById(Id);
            deleteSalarySettingDetailRs.IsDeleted = true;
            UpdateSalarySettingDetail(deleteSalarySettingDetailRs);
        }

        /// <summary>
        /// Update SalarySettingDetail into database
        /// </summary>
        /// <param name="SalarySettingDetail">SalarySettingDetail object</param>
        public void UpdateSalarySettingDetail(SalarySettingDetail SalarySettingDetail)
        {
            Context.Entry(SalarySettingDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
