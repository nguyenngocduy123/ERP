using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class CampaignRepository : GenericRepository<ErpCrmDbContext, Campaign>, ICampaignRepository
    {
        public CampaignRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Campaign
        /// </summary>
        /// <returns>Campaign list</returns>
        public IQueryable<Campaign> GetAllCampaign()
        {
            return Context.Campaign.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Campaign information by specific id
        /// </summary>
        /// <param name="CampaignId">Id of Campaign</param>
        /// <returns></returns>
        public Campaign GetCampaignById(int Id)
        {
            return Context.Campaign.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Campaign into database
        /// </summary>
        /// <param name="Campaign">Object infomation</param>
        public void InsertCampaign(Campaign Campaign)
        {
            Context.Campaign.Add(Campaign);
            Context.Entry(Campaign).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Campaign with specific id
        /// </summary>
        /// <param name="Id">Campaign Id</param>
        public void DeleteCampaign(int Id)
        {
            Campaign deletedCampaign = GetCampaignById(Id);
            Context.Campaign.Remove(deletedCampaign);
            Context.Entry(deletedCampaign).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Campaign with its Id and Update IsDeleted IF that Campaign has relationship with others
        /// </summary>
        /// <param name="CampaignId">Id of Campaign</param>
        public void DeleteCampaignRs(int Id)
        {
            Campaign deleteCampaignRs = GetCampaignById(Id);
            deleteCampaignRs.IsDeleted = true;
            UpdateCampaign(deleteCampaignRs);
        }

        /// <summary>
        /// Update Campaign into database
        /// </summary>
        /// <param name="Campaign">Campaign object</param>
        public void UpdateCampaign(Campaign Campaign)
        {
            Context.Entry(Campaign).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
