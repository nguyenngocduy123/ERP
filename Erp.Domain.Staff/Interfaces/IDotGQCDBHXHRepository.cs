using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDotGQCDBHXHRepository
    {
        /// <summary>
        /// Get all DotQGCDBHXH
        /// </summary>
        /// <returns>DotQGCDBHXH list</returns>
        IQueryable<DotGQCDBHXH> GetAllDotGQCDBHXH();

        /// <summary>
        /// Get DotQGCDBHXH information by specific id
        /// </summary>
        /// <param name="Id">Id of DotQGCDBHXH</param>
        /// <returns></returns>
        DotGQCDBHXH GetDotGQCDBHXHById(int Id);

        /// <summary>
        /// Insert DotQGCDBHXH into database
        /// </summary>
        /// <param name="DotGQCDBHXH">Object infomation</param>
        void InsertDotGQCDBHXH(DotGQCDBHXH DotGQCDBHXH);

        /// <summary>
        /// Delete DotQGCDBHXH with specific id
        /// </summary>
        /// <param name="Id">DotQGCDBHXH Id</param>
        void DeleteDotGQCDBHXH(int Id);

        /// <summary>
        /// Delete a DotQGCDBHXH with its Id and Update IsDeleted IF that DotQGCDBHXH has relationship with others
        /// </summary>
        /// <param name="Id">Id of DotQGCDBHXH</param>
        void DeleteDotGQCDBHXHRs(int Id);

        /// <summary>
        /// Update DotQGCDBHXH into database
        /// </summary>
        /// <param name="DotGQCDBHXH">DotQGCDBHXH object</param>
        void UpdateDotGQCDBHXH(DotGQCDBHXH DotGQCDBHXH);
    }
}
