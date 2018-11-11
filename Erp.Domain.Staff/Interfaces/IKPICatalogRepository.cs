using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IKPICatalogRepository
    {
        /// <summary>
        /// Get all KPICatalog
        /// </summary>
        /// <returns>KPICatalog list</returns>
        IQueryable<KPICatalog> GetAllKPICatalog();

        /// <summary>
        /// Get KPICatalog information by specific id
        /// </summary>
        /// <param name="Id">Id of KPICatalog</param>
        /// <returns></returns>
        KPICatalog GetKPICatalogById(int Id);

        /// <summary>
        /// Insert KPICatalog into database
        /// </summary>
        /// <param name="KPICatalog">Object infomation</param>
        void InsertKPICatalog(KPICatalog KPICatalog);

        /// <summary>
        /// Delete KPICatalog with specific id
        /// </summary>
        /// <param name="Id">KPICatalog Id</param>
        void DeleteKPICatalog(int Id);

        /// <summary>
        /// Delete a KPICatalog with its Id and Update IsDeleted IF that KPICatalog has relationship with others
        /// </summary>
        /// <param name="Id">Id of KPICatalog</param>
        void DeleteKPICatalogRs(int Id);

        /// <summary>
        /// Update KPICatalog into database
        /// </summary>
        /// <param name="KPICatalog">KPICatalog object</param>
        void UpdateKPICatalog(KPICatalog KPICatalog);
    }
}
