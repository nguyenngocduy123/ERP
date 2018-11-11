using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IWelfareProgramsDetailRepository
    {
        /// <summary>
        /// Get all WelfareProgramsDetail
        /// </summary>
        /// <returns>WelfareProgramsDetail list</returns>
        IQueryable<WelfareProgramsDetail> GetAllWelfareProgramsDetail();
        IQueryable<vwWelfareProgramsDetail> GetAllvwWelfareProgramsDetail();
        /// <summary>
        /// Get WelfareProgramsDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of WelfareProgramsDetail</param>
        /// <returns></returns>
        WelfareProgramsDetail GetWelfareProgramsDetailById(int Id);
        vwWelfareProgramsDetail GetvwWelfareProgramsDetailById(int Id);
        /// <summary>
        /// Insert WelfareProgramsDetail into database
        /// </summary>
        /// <param name="WelfareProgramsDetail">Object infomation</param>
        void InsertWelfareProgramsDetail(WelfareProgramsDetail WelfareProgramsDetail);

        /// <summary>
        /// Delete WelfareProgramsDetail with specific id
        /// </summary>
        /// <param name="Id">WelfareProgramsDetail Id</param>
        void DeleteWelfareProgramsDetail(int Id);

        /// <summary>
        /// Delete a WelfareProgramsDetail with its Id and Update IsDeleted IF that WelfareProgramsDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of WelfareProgramsDetail</param>
        void DeleteWelfareProgramsDetailRs(int Id);

        /// <summary>
        /// Update WelfareProgramsDetail into database
        /// </summary>
        /// <param name="WelfareProgramsDetail">WelfareProgramsDetail object</param>
        void UpdateWelfareProgramsDetail(WelfareProgramsDetail WelfareProgramsDetail);
    }
}
