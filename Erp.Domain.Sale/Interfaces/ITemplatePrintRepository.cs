using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ITemplatePrintRepository
    {
        /// <summary>
        /// Get all TemplatePrint
        /// </summary>
        /// <returns>TemplatePrint list</returns>
        IQueryable<TemplatePrint> GetAllTemplatePrint();

        /// <summary>
        /// Get TemplatePrint information by specific id
        /// </summary>
        /// <param name="Id">Id of TemplatePrint</param>
        /// <returns></returns>
        TemplatePrint GetTemplatePrintById(int Id);

        /// <summary>
        /// Insert TemplatePrint into database
        /// </summary>
        /// <param name="TemplatePrint">Object infomation</param>
        void InsertTemplatePrint(TemplatePrint TemplatePrint);

        /// <summary>
        /// Delete TemplatePrint with specific id
        /// </summary>
        /// <param name="Id">TemplatePrint Id</param>
        void DeleteTemplatePrint(int Id);

        /// <summary>
        /// Delete a TemplatePrint with its Id and Update IsDeleted IF that TemplatePrint has relationship with others
        /// </summary>
        /// <param name="Id">Id of TemplatePrint</param>
        void DeleteTemplatePrintRs(int Id);

        /// <summary>
        /// Update TemplatePrint into database
        /// </summary>
        /// <param name="TemplatePrint">TemplatePrint object</param>
        void UpdateTemplatePrint(TemplatePrint TemplatePrint);
    }
}
