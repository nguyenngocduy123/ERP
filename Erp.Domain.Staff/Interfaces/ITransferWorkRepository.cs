using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITransferWorkRepository
    {
        /// <summary>
        /// Get all TransferWork
        /// </summary>
        /// <returns>TransferWork list</returns>
        IQueryable<TransferWork> GetAllTransferWork();
        IQueryable<vwTransferWork> GetvwAllTransferWork();
        /// <summary>
        /// Get TransferWork information by specific id
        /// </summary>
        /// <param name="Id">Id of TransferWork</param>
        /// <returns></returns>
        TransferWork GetTransferWorkById(int Id);
        vwTransferWork GetvwTransferWorkById(int Id);
        /// <summary>
        /// Insert TransferWork into database
        /// </summary>
        /// <param name="TransferWork">Object infomation</param>
        void InsertTransferWork(TransferWork TransferWork);

        /// <summary>
        /// Delete TransferWork with specific id
        /// </summary>
        /// <param name="Id">TransferWork Id</param>
        void DeleteTransferWork(int Id);

        /// <summary>
        /// Delete a TransferWork with its Id and Update IsDeleted IF that TransferWork has relationship with others
        /// </summary>
        /// <param name="Id">Id of TransferWork</param>
        void DeleteTransferWorkRs(int Id);

        /// <summary>
        /// Update TransferWork into database
        /// </summary>
        /// <param name="TransferWork">TransferWork object</param>
        void UpdateTransferWork(TransferWork TransferWork);
    }
}
