using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalarySettingDetail_StaffRepository : GenericRepository<ErpStaffDbContext, SalarySettingDetail_Staff>, ISalarySettingDetail_StaffRepository
    {
        public SalarySettingDetail_StaffRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalarySettingDetail_Staff
        /// </summary>
        /// <returns>SalarySettingDetail_Staff list</returns>
        public IQueryable<SalarySettingDetail_Staff> GetAllSalarySettingDetail_Staff()
        {
            return Context.SalarySettingDetail_Staff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SalarySettingDetail_Staff information by specific id
        /// </summary>
        /// <param name="SalarySettingDetail_StaffId">Id of SalarySettingDetail_Staff</param>
        /// <returns></returns>
        public SalarySettingDetail_Staff GetSalarySettingDetail_StaffById(int Id)
        {
            return Context.SalarySettingDetail_Staff.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public SalarySettingDetail_Staff GetSalarySettingDetail_StaffBySetting(int SettingId, int staffId, int settingDetailId)
        {
            return Context.SalarySettingDetail_Staff.SingleOrDefault(item => item.SalarySettingId == SettingId &&item.StaffId==staffId&&item.SalarySettingDetailId==settingDetailId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert SalarySettingDetail_Staff into database
        /// </summary>
        /// <param name="SalarySettingDetail_Staff">Object infomation</param>
        public void InsertSalarySettingDetail_Staff(SalarySettingDetail_Staff SalarySettingDetail_Staff)
        {
            Context.SalarySettingDetail_Staff.Add(SalarySettingDetail_Staff);
            Context.Entry(SalarySettingDetail_Staff).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalarySettingDetail_Staff with specific id
        /// </summary>
        /// <param name="Id">SalarySettingDetail_Staff Id</param>
        public void DeleteSalarySettingDetail_Staff(int Id)
        {
            SalarySettingDetail_Staff deletedSalarySettingDetail_Staff = GetSalarySettingDetail_StaffById(Id);
            Context.SalarySettingDetail_Staff.Remove(deletedSalarySettingDetail_Staff);
            Context.Entry(deletedSalarySettingDetail_Staff).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a SalarySettingDetail_Staff with its Id and Update IsDeleted IF that SalarySettingDetail_Staff has relationship with others
        /// </summary>
        /// <param name="SalarySettingDetail_StaffId">Id of SalarySettingDetail_Staff</param>
        public void DeleteSalarySettingDetail_StaffRs(int Id)
        {
            SalarySettingDetail_Staff deleteSalarySettingDetail_StaffRs = GetSalarySettingDetail_StaffById(Id);
            deleteSalarySettingDetail_StaffRs.IsDeleted = true;
            UpdateSalarySettingDetail_Staff(deleteSalarySettingDetail_StaffRs);
        }

        /// <summary>
        /// Update SalarySettingDetail_Staff into database
        /// </summary>
        /// <param name="SalarySettingDetail_Staff">SalarySettingDetail_Staff object</param>
        public void UpdateSalarySettingDetail_Staff(SalarySettingDetail_Staff SalarySettingDetail_Staff)
        {
            Context.Entry(SalarySettingDetail_Staff).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
