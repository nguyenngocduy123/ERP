using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class StaffSocialInsuranceRepository : GenericRepository<ErpStaffDbContext, StaffSocialInsurance>, IStaffSocialInsuranceRepository
    {
        public StaffSocialInsuranceRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all StaffSocialInsurance
        /// </summary>
        /// <returns>StaffSocialInsurance list</returns>
        public IQueryable<StaffSocialInsurance> GetAllStaffSocialInsurance()
        {
            return Context.StaffSocialInsurance.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwStaffSocialInsurance> GetAllViewStaffSocialInsurance()
        {
            return Context.vwStaffSocialInsurance.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwStaffSocialInsurance GetAllViewStaffSocialInsuranceById(int id)
        {
            return Context.vwStaffSocialInsurance.SingleOrDefault(item => item.Id == id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public StaffSocialInsurance GetStaffSocialInsuranceByStaffId(int StaffId)
        {
            return Context.StaffSocialInsurance.OrderBy(item=> item.CreatedDate).FirstOrDefault(item => item.StaffId == StaffId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get StaffSocialInsurance information by specific id
        /// </summary>
        /// <param name="StaffSocialInsuranceId">Id of StaffSocialInsurance</param>
        /// <returns></returns>
        public StaffSocialInsurance GetStaffSocialInsuranceById(int Id)
        {
            return Context.StaffSocialInsurance.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwStaffSocialInsurance GetvwStaffSocialInsuranceById(int Id)
        {
            return Context.vwStaffSocialInsurance.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert StaffSocialInsurance into database
        /// </summary>
        /// <param name="StaffSocialInsurance">Object infomation</param>
        public void InsertStaffSocialInsurance(StaffSocialInsurance StaffSocialInsurance)
        {
            Context.StaffSocialInsurance.Add(StaffSocialInsurance);
            Context.Entry(StaffSocialInsurance).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete StaffSocialInsurance with specific id
        /// </summary>
        /// <param name="Id">StaffSocialInsurance Id</param>
        public void DeleteStaffSocialInsurance(int Id)
        {
            StaffSocialInsurance deletedStaffSocialInsurance = GetStaffSocialInsuranceById(Id);
            Context.StaffSocialInsurance.Remove(deletedStaffSocialInsurance);
            Context.Entry(deletedStaffSocialInsurance).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a StaffSocialInsurance with its Id and Update IsDeleted IF that StaffSocialInsurance has relationship with others
        /// </summary>
        /// <param name="StaffSocialInsuranceId">Id of StaffSocialInsurance</param>
        public void DeleteStaffSocialInsuranceRs(int Id)
        {
            StaffSocialInsurance deleteStaffSocialInsuranceRs = GetStaffSocialInsuranceById(Id);
            deleteStaffSocialInsuranceRs.IsDeleted = true;
            UpdateStaffSocialInsurance(deleteStaffSocialInsuranceRs);
        }

        /// <summary>
        /// Update StaffSocialInsurance into database
        /// </summary>
        /// <param name="StaffSocialInsurance">StaffSocialInsurance object</param>
        public void UpdateStaffSocialInsurance(StaffSocialInsurance StaffSocialInsurance)
        {
            Context.Entry(StaffSocialInsurance).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
