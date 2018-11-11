using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ILabourContractRepository
    {
        /// <summary>
        /// Get all LabourContract
        /// </summary>
        /// <returns>LabourContract list</returns>
        IQueryable<LabourContract> GetAllLabourContract();
        IQueryable<vwLabourContract> GetAllvwLabourContract();
        /// <summary>
        /// Get LabourContract information by specific id
        /// </summary>
        /// <param name="Id">Id of LabourContract</param>
        /// <returns></returns>
        LabourContract GetLabourContractById(int? Id);
        vwLabourContract GetvwLabourContractById(int Id);
        /// <summary>
        /// Insert LabourContract into database
        /// </summary>
        /// <param name="LabourContract">Object infomation</param>
        void InsertLabourContract(LabourContract LabourContract);

        /// <summary>
        /// Delete LabourContract with specific id
        /// </summary>
        /// <param name="Id">LabourContract Id</param>
        void DeleteLabourContract(int Id);

        /// <summary>
        /// Delete a LabourContract with its Id and Update IsDeleted IF that LabourContract has relationship with others
        /// </summary>
        /// <param name="Id">Id of LabourContract</param>
        void DeleteLabourContractRs(int Id);

        /// <summary>
        /// Update LabourContract into database
        /// </summary>
        /// <param name="LabourContract">LabourContract object</param>
        void UpdateLabourContract(LabourContract LabourContract);
    }
}
