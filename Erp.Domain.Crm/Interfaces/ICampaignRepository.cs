using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface ICampaignRepository
    {
        /// <summary>
        /// Get all Campaign
        /// </summary>
        /// <returns>Campaign list</returns>
        IQueryable<Campaign> GetAllCampaign();

        /// <summary>
        /// Get Campaign information by specific id
        /// </summary>
        /// <param name="Id">Id of Campaign</param>
        /// <returns></returns>
        Campaign GetCampaignById(int Id);

        /// <summary>
        /// Insert Campaign into database
        /// </summary>
        /// <param name="Campaign">Object infomation</param>
        void InsertCampaign(Campaign Campaign);

        /// <summary>
        /// Delete Campaign with specific id
        /// </summary>
        /// <param name="Id">Campaign Id</param>
        void DeleteCampaign(int Id);

        /// <summary>
        /// Delete a Campaign with its Id and Update IsDeleted IF that Campaign has relationship with others
        /// </summary>
        /// <param name="Id">Id of Campaign</param>
        void DeleteCampaignRs(int Id);

        /// <summary>
        /// Update Campaign into database
        /// </summary>
        /// <param name="Campaign">Campaign object</param>
        void UpdateCampaign(Campaign Campaign);
    }
}
