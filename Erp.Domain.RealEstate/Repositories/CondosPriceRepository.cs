using Erp.Domain.RealEstate.Entities;
using Erp.Domain.RealEstate.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.RealEstate.Repositories
{
    public class CondosPriceRepository : GenericRepository<ErpRealEstateDbContext, CondosPrice>, ICondosPriceRepository
    {
        public CondosPriceRepository(ErpRealEstateDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CondosPrice
        /// </summary>
        /// <returns>CondosPrice list</returns>
        public IQueryable<CondosPrice> GetAllCondosPrice()
        {
            return Context.CondosPrice.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get CondosPrice information by specific id
        /// </summary>
        /// <param name="CondosPriceId">Id of CondosPrice</param>
        /// <returns></returns>
        public CondosPrice GetCondosPriceById(int Id)
        {
            return Context.CondosPrice.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert CondosPrice into database
        /// </summary>
        /// <param name="CondosPrice">Object infomation</param>
        public void InsertCondosPrice(CondosPrice CondosPrice)
        {
            Context.CondosPrice.Add(CondosPrice);
            Context.Entry(CondosPrice).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CondosPrice with specific id
        /// </summary>
        /// <param name="Id">CondosPrice Id</param>
        public void DeleteCondosPrice(int Id)
        {
            CondosPrice deletedCondosPrice = GetCondosPriceById(Id);
            Context.CondosPrice.Remove(deletedCondosPrice);
            Context.Entry(deletedCondosPrice).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CondosPrice with its Id and Update IsDeleted IF that CondosPrice has relationship with others
        /// </summary>
        /// <param name="CondosPriceId">Id of CondosPrice</param>
        public void DeleteCondosPriceRs(int Id)
        {
            CondosPrice deleteCondosPriceRs = GetCondosPriceById(Id);
            deleteCondosPriceRs.IsDeleted = true;
            UpdateCondosPrice(deleteCondosPriceRs);
        }

        /// <summary>
        /// Update CondosPrice into database
        /// </summary>
        /// <param name="CondosPrice">CondosPrice object</param>
        public void UpdateCondosPrice(CondosPrice CondosPrice)
        {
            Context.Entry(CondosPrice).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
