using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ReceiptDetailRepository : GenericRepository<ErpAccountDbContext, ReceiptDetail>, IReceiptDetailRepository
    {
        public ReceiptDetailRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ReceiptDetail
        /// </summary>
        /// <returns>ReceiptDetail list</returns>
        public IQueryable<ReceiptDetail> GetAllReceiptDetail()
        {
            return Context.ReceiptDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<ReceiptDetail> GetAllReceiptDetailFull()
        {
            return Context.ReceiptDetail;
        }
        /// <summary>
        /// Get ReceiptDetail information by specific id
        /// </summary>
        /// <param name="ReceiptDetailId">Id of ReceiptDetail</param>
        /// <returns></returns>
        public ReceiptDetail GetReceiptDetailById(int Id)
        {
            return Context.ReceiptDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public ReceiptDetail GetReceiptDetailByReceiptId(int ReceiptId)
        {
            return Context.ReceiptDetail.SingleOrDefault(item => item.ReceiptId == ReceiptId && (item.IsDeleted == null || item.IsDeleted == false));
        }


        public IQueryable<ReceiptDetail> GetAllReceiptDetailByReceiptId(int ReceiptId)
        {
            return Context.ReceiptDetail.Where(item => item.ReceiptId == ReceiptId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">Object infomation</param>
        public void InsertReceiptDetail(ReceiptDetail ReceiptDetail)
        {
            Context.ReceiptDetail.Add(ReceiptDetail);
            Context.Entry(ReceiptDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ReceiptDetail with specific id
        /// </summary>
        /// <param name="Id">ReceiptDetail Id</param>
        public void DeleteReceiptDetail(int Id)
        {
            ReceiptDetail deletedReceiptDetail = GetReceiptDetailById(Id);
            Context.ReceiptDetail.Remove(deletedReceiptDetail);
            Context.Entry(deletedReceiptDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ReceiptDetail with its Id and Update IsDeleted IF that ReceiptDetail has relationship with others
        /// </summary>
        /// <param name="ReceiptDetailId">Id of ReceiptDetail</param>
        public void DeleteReceiptDetailRs(int Id)
        {
            ReceiptDetail deleteReceiptDetailRs = GetReceiptDetailById(Id);
            deleteReceiptDetailRs.IsDeleted = true;
            UpdateReceiptDetail(deleteReceiptDetailRs);
        }

        /// <summary>
        /// Update ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">ReceiptDetail object</param>
        public void UpdateReceiptDetail(ReceiptDetail ReceiptDetail)
        {
            Context.Entry(ReceiptDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
