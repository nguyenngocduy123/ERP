using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDotBCBHXHRepository
    {
        /// <summary>
        /// Get all DotBCBHXH
        /// </summary>
        /// <returns>DotBCBHXH list</returns>
        IQueryable<DotBCBHXH> GetAllDotBCBHXH();

        /// <summary>
        /// Get DotBCBHXH information by specific id
        /// </summary>
        /// <param name="Id">Id of DotBCBHXH</param>
        /// <returns></returns>
        DotBCBHXH GetDotBCBHXHById(int Id);

        /// <summary>
        /// Insert DotBCBHXH into database
        /// </summary>
        /// <param name="DotBCBHXH">Object infomation</param>
        void InsertDotBCBHXH(DotBCBHXH DotBCBHXH);

        /// <summary>
        /// Delete DotBCBHXH with specific id
        /// </summary>
        /// <param name="Id">DotBCBHXH Id</param>
        void DeleteDotBCBHXH(int Id);

        /// <summary>
        /// Delete a DotBCBHXH with its Id and Update IsDeleted IF that DotBCBHXH has relationship with others
        /// </summary>
        /// <param name="Id">Id of DotBCBHXH</param>
        void DeleteDotBCBHXHRs(int Id);

        /// <summary>
        /// Update DotBCBHXH into database
        /// </summary>
        /// <param name="DotBCBHXH">DotBCBHXH object</param>
        void UpdateDotBCBHXH(DotBCBHXH DotBCBHXH);
    }
}
