using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ILogDocumentAttributeRepository
    {
        /// <summary>
        /// Get all LogDocumentAttribute
        /// </summary>
        /// <returns>LogDocumentAttribute list</returns>
        IQueryable<LogDocumentAttribute> GetAllLogDocumentAttribute();
        IQueryable<vwLogDocumentAttribute> GetAllvwLogDocumentAttribute();
        /// <summary>
        /// Get LogDocumentAttribute information by specific id
        /// </summary>
        /// <param name="Id">Id of LogDocumentAttribute</param>
        /// <returns></returns>
        LogDocumentAttribute GetLogDocumentAttributeById(int Id);
        vwLogDocumentAttribute GetvwLogDocumentAttributeById(int Id);
        /// <summary>
        /// Insert LogDocumentAttribute into database
        /// </summary>
        /// <param name="LogDocumentAttribute">Object infomation</param>
        void InsertLogDocumentAttribute(LogDocumentAttribute LogDocumentAttribute);

        /// <summary>
        /// Delete LogDocumentAttribute with specific id
        /// </summary>
        /// <param name="Id">LogDocumentAttribute Id</param>
        void DeleteLogDocumentAttribute(int Id);

        /// <summary>
        /// Delete a LogDocumentAttribute with its Id and Update IsDeleted IF that LogDocumentAttribute has relationship with others
        /// </summary>
        /// <param name="Id">Id of LogDocumentAttribute</param>
        void DeleteLogDocumentAttributeRs(int Id);

        /// <summary>
        /// Update LogDocumentAttribute into database
        /// </summary>
        /// <param name="LogDocumentAttribute">LogDocumentAttribute object</param>
        void UpdateLogDocumentAttribute(LogDocumentAttribute LogDocumentAttribute);
    }
}
