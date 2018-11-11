using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDotGQCDBHXHDetailRepository
    {
        /// <summary>
        /// Get all DotGQCDBHXHDetail
        /// </summary>
        /// <returns>DotGQCDBHXHDetail list</returns>
        IQueryable<DotGQCDBHXHDetail> GetAllDotGQCDBHXHDetail();
        IQueryable<vwDotGQCDBHXHDetail> GetAllvwDotGQCDBHXHDetail();
        /// <summary>
        /// Get DotGQCDBHXHDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of DotGQCDBHXHDetail</param>
        /// <returns></returns>
        DotGQCDBHXHDetail GetDotGQCDBHXHDetailById(int Id);
        vwDotGQCDBHXHDetail GetvwDotGQCDBHXHDetailById(int Id);
        /// <summary>
        /// Insert DotGQCDBHXHDetail into database
        /// </summary>
        /// <param name="DotGQCDBHXHDetail">Object infomation</param>
        void InsertDotGQCDBHXHDetail(DotGQCDBHXHDetail DotGQCDBHXHDetail);

        /// <summary>
        /// Delete DotGQCDBHXHDetail with specific id
        /// </summary>
        /// <param name="Id">DotGQCDBHXHDetail Id</param>
        void DeleteDotGQCDBHXHDetail(int Id);

        /// <summary>
        /// Delete a DotGQCDBHXHDetail with its Id and Update IsDeleted IF that DotGQCDBHXHDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of DotGQCDBHXHDetail</param>
        void DeleteDotGQCDBHXHDetailRs(int Id);

        /// <summary>
        /// Update DotGQCDBHXHDetail into database
        /// </summary>
        /// <param name="DotGQCDBHXHDetail">DotGQCDBHXHDetail object</param>
        void UpdateDotGQCDBHXHDetail(DotGQCDBHXHDetail DotGQCDBHXHDetail);
    }
}
