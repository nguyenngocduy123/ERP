using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDotBCBHXHDetailRepository
    {
        /// <summary>
        /// Get all DotBCBHXHDetail
        /// </summary>
        /// <returns>DotBCBHXHDetail list</returns>
        IQueryable<DotBCBHXHDetail> GetAllDotBCBHXHDetail();
        IQueryable<vwDotBCBHXHDetail> GetAllvwDotBCBHXHDetailByDotBCBHXHId(int DotBCBHXHId);
        /// <summary>
        /// Get DotBCBHXHDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of DotBCBHXHDetail</param>
        /// <returns></returns>
        DotBCBHXHDetail GetDotBCBHXHDetailById(int Id);

        /// <summary>
        /// Insert DotBCBHXHDetail into database
        /// </summary>
        /// <param name="DotBCBHXHDetail">Object infomation</param>
        void InsertDotBCBHXHDetail(DotBCBHXHDetail DotBCBHXHDetail);

        /// <summary>
        /// Delete DotBCBHXHDetail with specific id
        /// </summary>
        /// <param name="Id">DotBCBHXHDetail Id</param>
        void DeleteDotBCBHXHDetail(int Id);

        /// <summary>
        /// Delete a DotBCBHXHDetail with its Id and Update IsDeleted IF that DotBCBHXHDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of DotBCBHXHDetail</param>
        void DeleteDotBCBHXHDetailRs(int Id);

        /// <summary>
        /// Update DotBCBHXHDetail into database
        /// </summary>
        /// <param name="DotBCBHXHDetail">DotBCBHXHDetail object</param>
        void UpdateDotBCBHXHDetail(DotBCBHXHDetail DotBCBHXHDetail);
        IQueryable<vwDotBCBHXHDetail> GetAllViewDotBCBHXHDetail();
    }
}
