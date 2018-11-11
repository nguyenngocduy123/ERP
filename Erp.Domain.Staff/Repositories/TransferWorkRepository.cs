using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TransferWorkRepository : GenericRepository<ErpStaffDbContext, TransferWork>, ITransferWorkRepository
    {
        public TransferWorkRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TransferWork
        /// </summary>
        /// <returns>TransferWork list</returns>
        public IQueryable<TransferWork> GetAllTransferWork()
        {
            return Context.TransferWork.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTransferWork> GetvwAllTransferWork()
        {
            return Context.vwTransferWork.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get TransferWork information by specific id
        /// </summary>
        /// <param name="TransferWorkId">Id of TransferWork</param>
        /// <returns></returns>
        public TransferWork GetTransferWorkById(int Id)
        {
            return Context.TransferWork.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwTransferWork GetvwTransferWorkById(int Id)
        {
            return Context.vwTransferWork.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert TransferWork into database
        /// </summary>
        /// <param name="TransferWork">Object infomation</param>
        public void InsertTransferWork(TransferWork TransferWork)
        {
            Context.TransferWork.Add(TransferWork);
            Context.Entry(TransferWork).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TransferWork with specific id
        /// </summary>
        /// <param name="Id">TransferWork Id</param>
        public void DeleteTransferWork(int Id)
        {
            TransferWork deletedTransferWork = GetTransferWorkById(Id);
            Context.TransferWork.Remove(deletedTransferWork);
            Context.Entry(deletedTransferWork).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TransferWork with its Id and Update IsDeleted IF that TransferWork has relationship with others
        /// </summary>
        /// <param name="TransferWorkId">Id of TransferWork</param>
        public void DeleteTransferWorkRs(int Id)
        {
            TransferWork deleteTransferWorkRs = GetTransferWorkById(Id);
            deleteTransferWorkRs.IsDeleted = true;
            UpdateTransferWork(deleteTransferWorkRs);
        }

        /// <summary>
        /// Update TransferWork into database
        /// </summary>
        /// <param name="TransferWork">TransferWork object</param>
        public void UpdateTransferWork(TransferWork TransferWork)
        {
            Context.Entry(TransferWork).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
