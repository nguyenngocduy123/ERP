using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IUsingServiceLogDetailRepository
    {
        /// <summary>
        /// Get all UsingServiceLogDetail
        /// </summary>
        /// <returns>UsingServiceLogDetail list</returns>
        IQueryable<UsingServiceLogDetail> GetAllUsingServiceLogDetail();
        IQueryable<vwUsingServiceLogDetail> GetAllvwUsingServiceLogDetail();
        /// <summary>
        /// Get UsingServiceLogDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of UsingServiceLogDetail</param>
        /// <returns></returns>
        UsingServiceLogDetail GetUsingServiceLogDetailById(int Id);
        vwUsingServiceLogDetail GetvwUsingServiceLogDetailById(int Id);
        /// <summary>
        /// Insert UsingServiceLogDetail into database
        /// </summary>
        /// <param name="UsingServiceLogDetail">Object infomation</param>
        void InsertUsingServiceLogDetail(UsingServiceLogDetail UsingServiceLogDetail);

        /// <summary>
        /// Delete UsingServiceLogDetail with specific id
        /// </summary>
        /// <param name="Id">UsingServiceLogDetail Id</param>
        void DeleteUsingServiceLogDetail(int Id);

        /// <summary>
        /// Delete a UsingServiceLogDetail with its Id and Update IsDeleted IF that UsingServiceLogDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of UsingServiceLogDetail</param>
        void DeleteUsingServiceLogDetailRs(int Id);

        /// <summary>
        /// Update UsingServiceLogDetail into database
        /// </summary>
        /// <param name="UsingServiceLogDetail">UsingServiceLogDetail object</param>
        void UpdateUsingServiceLogDetail(UsingServiceLogDetail UsingServiceLogDetail);
    }
}
