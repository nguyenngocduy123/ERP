using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class TemplatePrintRepository : GenericRepository<ErpSaleDbContext, TemplatePrint>, ITemplatePrintRepository
    {
        public TemplatePrintRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TemplatePrint
        /// </summary>
        /// <returns>TemplatePrint list</returns>
        public IQueryable<TemplatePrint> GetAllTemplatePrint()
        {
            return Context.TemplatePrint.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TemplatePrint information by specific id
        /// </summary>
        /// <param name="TemplatePrintId">Id of TemplatePrint</param>
        /// <returns></returns>
        public TemplatePrint GetTemplatePrintById(int Id)
        {
            return Context.TemplatePrint.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert TemplatePrint into database
        /// </summary>
        /// <param name="TemplatePrint">Object infomation</param>
        public void InsertTemplatePrint(TemplatePrint TemplatePrint)
        {
            Context.TemplatePrint.Add(TemplatePrint);
            Context.Entry(TemplatePrint).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TemplatePrint with specific id
        /// </summary>
        /// <param name="Id">TemplatePrint Id</param>
        public void DeleteTemplatePrint(int Id)
        {
            TemplatePrint deletedTemplatePrint = GetTemplatePrintById(Id);
            Context.TemplatePrint.Remove(deletedTemplatePrint);
            Context.Entry(deletedTemplatePrint).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TemplatePrint with its Id and Update IsDeleted IF that TemplatePrint has relationship with others
        /// </summary>
        /// <param name="TemplatePrintId">Id of TemplatePrint</param>
        public void DeleteTemplatePrintRs(int Id)
        {
            TemplatePrint deleteTemplatePrintRs = GetTemplatePrintById(Id);
            deleteTemplatePrintRs.IsDeleted = true;
            UpdateTemplatePrint(deleteTemplatePrintRs);
        }

        /// <summary>
        /// Update TemplatePrint into database
        /// </summary>
        /// <param name="TemplatePrint">TemplatePrint object</param>
        public void UpdateTemplatePrint(TemplatePrint TemplatePrint)
        {
            Context.Entry(TemplatePrint).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
