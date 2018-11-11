using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalarySettingRepository : GenericRepository<ErpStaffDbContext, SalarySetting>, ISalarySettingRepository
    {
        public SalarySettingRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalarySetting
        /// </summary>
        /// <returns>SalarySetting list</returns>
        public IQueryable<SalarySetting> GetAllSalarySetting()
        {
            return Context.SalarySetting.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SalarySetting information by specific id
        /// </summary>
        /// <param name="SalarySettingId">Id of SalarySetting</param>
        /// <returns></returns>
        public SalarySetting GetSalarySettingById(int Id)
        {
            return Context.SalarySetting.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
       

        /// <summary>
        /// Insert SalarySetting into database
        /// </summary>
        /// <param name="SalarySetting">Object infomation</param>
        public void InsertSalarySetting(SalarySetting SalarySetting)
        {
            Context.SalarySetting.Add(SalarySetting);
            Context.Entry(SalarySetting).State = EntityState.Added;
            Context.SaveChanges();
        }
       

        /// <summary>
        /// Delete SalarySetting with specific id
        /// </summary>
        /// <param name="Id">SalarySetting Id</param>
        public void DeleteSalarySetting(int Id)
        {
            SalarySetting deletedSalarySetting = GetSalarySettingById(Id);
            Context.SalarySetting.Remove(deletedSalarySetting);
            Context.Entry(deletedSalarySetting).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a SalarySetting with its Id and Update IsDeleted IF that SalarySetting has relationship with others
        /// </summary>
        /// <param name="SalarySettingId">Id of SalarySetting</param>
        public void DeleteSalarySettingRs(int Id)
        {
            SalarySetting deleteSalarySettingRs = GetSalarySettingById(Id);
            deleteSalarySettingRs.IsDeleted = true;
            UpdateSalarySetting(deleteSalarySettingRs);
        }

        /// <summary>
        /// Update SalarySetting into database
        /// </summary>
        /// <param name="SalarySetting">SalarySetting object</param>
        public void UpdateSalarySetting(SalarySetting SalarySetting)
        {
            Context.Entry(SalarySetting).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
