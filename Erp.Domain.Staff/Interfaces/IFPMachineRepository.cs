using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IFPMachineRepository
    {
        /// <summary>
        /// Get all FPMachine
        /// </summary>
        /// <returns>FPMachine list</returns>
        IQueryable<FPMachine> GetAllFPMachine();
        IQueryable<FingerPrint> GetAllFingerPrint();
        IQueryable<vwFingerPrint> GetAllvwFingerPrint();
        /// <summary>
        /// Get FPMachine information by specific id
        /// </summary>
        /// <param name="Id">Id of FPMachine</param>
        /// <returns></returns>
        FPMachine GetFPMachineById(int Id);

        /// <summary>
        /// Insert FPMachine into database
        /// </summary>
        /// <param name="FPMachine">Object infomation</param>
        void InsertFPMachine(FPMachine FPMachine);

        /// <summary>
        /// Delete FPMachine with specific id
        /// </summary>
        /// <param name="Id">FPMachine Id</param>
        void DeleteFPMachine(int Id);

        /// <summary>
        /// Delete a FPMachine with its Id and Update IsDeleted IF that FPMachine has relationship with others
        /// </summary>
        /// <param name="Id">Id of FPMachine</param>
        void DeleteFPMachineRs(int Id);

        /// <summary>
        /// Update FPMachine into database
        /// </summary>
        /// <param name="FPMachine">FPMachine object</param>
        void UpdateFPMachine(FPMachine FPMachine);
        void UpdateFingerPrint(FingerPrint FingerPrint);
    }
}
