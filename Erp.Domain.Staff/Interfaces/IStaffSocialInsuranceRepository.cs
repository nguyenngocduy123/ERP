using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IStaffSocialInsuranceRepository
    {
        /// <summary>
        /// Get all StaffSocialInsurance
        /// </summary>
        /// <returns>StaffSocialInsurance list</returns>
        IQueryable<StaffSocialInsurance> GetAllStaffSocialInsurance();
        IQueryable<vwStaffSocialInsurance> GetAllViewStaffSocialInsurance();

        /// <summary>
        /// Get StaffSocialInsurance information by specific id
        /// </summary>
        /// <param name="Id">Id of StaffSocialInsurance</param>
        /// <returns></returns>
        StaffSocialInsurance GetStaffSocialInsuranceById(int Id);
        vwStaffSocialInsurance GetvwStaffSocialInsuranceById(int Id);
        StaffSocialInsurance GetStaffSocialInsuranceByStaffId(int StaffId);
        /// <summary>
        /// Insert StaffSocialInsurance into database
        /// </summary>
        /// <param name="StaffSocialInsurance">Object infomation</param>
        void InsertStaffSocialInsurance(StaffSocialInsurance StaffSocialInsurance);

        /// <summary>
        /// Delete StaffSocialInsurance with specific id
        /// </summary>
        /// <param name="Id">StaffSocialInsurance Id</param>
        void DeleteStaffSocialInsurance(int Id);

        /// <summary>
        /// Delete a StaffSocialInsurance with its Id and Update IsDeleted IF that StaffSocialInsurance has relationship with others
        /// </summary>
        /// <param name="Id">Id of StaffSocialInsurance</param>
        void DeleteStaffSocialInsuranceRs(int Id);

        /// <summary>
        /// Update StaffSocialInsurance into database
        /// </summary>
        /// <param name="StaffSocialInsurance">StaffSocialInsurance object</param>
        void UpdateStaffSocialInsurance(StaffSocialInsurance StaffSocialInsurance);

        vwStaffSocialInsurance GetAllViewStaffSocialInsuranceById(int id);
    }
}
