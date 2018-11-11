using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ILabourContractTypeRepository
    {
        /// <summary>
        /// Get all LabourContractType
        /// </summary>
        /// <returns>LabourContractType list</returns>
        IQueryable<LabourContractType> GetAllLabourContractType();

        /// <summary>
        /// Get LabourContractType information by specific id
        /// </summary>
        /// <param name="Id">Id of LabourContractType</param>
        /// <returns></returns>
        LabourContractType GetLabourContractTypeById(int Id);

        /// <summary>
        /// Insert LabourContractType into database
        /// </summary>
        /// <param name="LabourContractType">Object infomation</param>
        void InsertLabourContractType(LabourContractType LabourContractType);

        /// <summary>
        /// Delete LabourContractType with specific id
        /// </summary>
        /// <param name="Id">LabourContractType Id</param>
        void DeleteLabourContractType(int Id);

        /// <summary>
        /// Delete a LabourContractType with its Id and Update IsDeleted IF that LabourContractType has relationship with others
        /// </summary>
        /// <param name="Id">Id of LabourContractType</param>
        void DeleteLabourContractTypeRs(int Id);

        /// <summary>
        /// Update LabourContractType into database
        /// </summary>
        /// <param name="LabourContractType">LabourContractType object</param>
        void UpdateLabourContractType(LabourContractType LabourContractType);
    }
}
