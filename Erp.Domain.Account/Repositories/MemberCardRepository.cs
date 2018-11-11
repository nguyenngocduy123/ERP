using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class MemberCardRepository : GenericRepository<ErpAccountDbContext, MemberCard>, IMemberCardRepository
    {
        public MemberCardRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all MemberCard
        /// </summary>
        /// <returns>MemberCard list</returns>
        public IQueryable<MemberCard> GetAllMemberCard()
        {
            return Context.MemberCard.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwMemberCard> GetAllvwMemberCard()
        {
            return Context.vwMemberCard.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get MemberCard information by specific id
        /// </summary>
        /// <param name="MemberCardId">Id of MemberCard</param>
        /// <returns></returns>
        public MemberCard GetMemberCardById(int Id)
        {
            return Context.MemberCard.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwMemberCard GetvwMemberCardById(int Id)
        {
            return Context.vwMemberCard.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert MemberCard into database
        /// </summary>
        /// <param name="MemberCard">Object infomation</param>
        public void InsertMemberCard(MemberCard MemberCard)
        {
            Context.MemberCard.Add(MemberCard);
            Context.Entry(MemberCard).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete MemberCard with specific id
        /// </summary>
        /// <param name="Id">MemberCard Id</param>
        public void DeleteMemberCard(int Id)
        {
            MemberCard deletedMemberCard = GetMemberCardById(Id);
            Context.MemberCard.Remove(deletedMemberCard);
            Context.Entry(deletedMemberCard).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a MemberCard with its Id and Update IsDeleted IF that MemberCard has relationship with others
        /// </summary>
        /// <param name="MemberCardId">Id of MemberCard</param>
        public void DeleteMemberCardRs(int Id)
        {
            MemberCard deleteMemberCardRs = GetMemberCardById(Id);
            deleteMemberCardRs.IsDeleted = true;
            UpdateMemberCard(deleteMemberCardRs);
        }

        /// <summary>
        /// Update MemberCard into database
        /// </summary>
        /// <param name="MemberCard">MemberCard object</param>
        public void UpdateMemberCard(MemberCard MemberCard)
        {
            Context.Entry(MemberCard).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
