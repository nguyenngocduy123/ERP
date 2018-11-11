using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class MemberCardDetailRepository : GenericRepository<ErpAccountDbContext, MemberCardDetail>, IMemberCardDetailRepository
    {
        public MemberCardDetailRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all MemberCardDetail
        /// </summary>
        /// <returns>MemberCardDetail list</returns>
        public IQueryable<MemberCardDetail> GetAllMemberCardDetail()
        {
            return Context.MemberCardDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get MemberCardDetail information by specific id
        /// </summary>
        /// <param name="MemberCardDetailId">Id of MemberCardDetail</param>
        /// <returns></returns>
        public MemberCardDetail GetMemberCardDetailById(int Id)
        {
            return Context.MemberCardDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert MemberCardDetail into database
        /// </summary>
        /// <param name="MemberCardDetail">Object infomation</param>
        public void InsertMemberCardDetail(MemberCardDetail MemberCardDetail)
        {
            Context.MemberCardDetail.Add(MemberCardDetail);
            Context.Entry(MemberCardDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete MemberCardDetail with specific id
        /// </summary>
        /// <param name="Id">MemberCardDetail Id</param>
        public void DeleteMemberCardDetail(int Id)
        {
            MemberCardDetail deletedMemberCardDetail = GetMemberCardDetailById(Id);
            Context.MemberCardDetail.Remove(deletedMemberCardDetail);
            Context.Entry(deletedMemberCardDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a MemberCardDetail with its Id and Update IsDeleted IF that MemberCardDetail has relationship with others
        /// </summary>
        /// <param name="MemberCardDetailId">Id of MemberCardDetail</param>
        public void DeleteMemberCardDetailRs(int Id)
        {
            MemberCardDetail deleteMemberCardDetailRs = GetMemberCardDetailById(Id);
            deleteMemberCardDetailRs.IsDeleted = true;
            UpdateMemberCardDetail(deleteMemberCardDetailRs);
        }

        /// <summary>
        /// Update MemberCardDetail into database
        /// </summary>
        /// <param name="MemberCardDetail">MemberCardDetail object</param>
        public void UpdateMemberCardDetail(MemberCardDetail MemberCardDetail)
        {
            Context.Entry(MemberCardDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
