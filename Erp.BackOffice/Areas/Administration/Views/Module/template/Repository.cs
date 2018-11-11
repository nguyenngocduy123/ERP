using <APP_NAME>.Domain.<AREA_NAME>.Entities;
using <APP_NAME>.Domain.<AREA_NAME>.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace <APP_NAME>.Domain.<AREA_NAME>.Repositories
{
    public class <MODULE_NAME>Repository : GenericRepository<<APP_NAME><AREA_NAME>DbContext, <MODULE_NAME>>, I<MODULE_NAME>Repository
    {
        public <MODULE_NAME>Repository(<APP_NAME><AREA_NAME>DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all <MODULE_NAME>
        /// </summary>
        /// <returns><MODULE_NAME> list</returns>
        public IQueryable<<MODULE_NAME>> GetAll<MODULE_NAME>()
        {
            return Context.<MODULE_NAME>.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get <MODULE_NAME> information by specific id
        /// </summary>
        /// <param name="<MODULE_NAME>Id">Id of <MODULE_NAME></param>
        /// <returns></returns>
        public <MODULE_NAME> Get<MODULE_NAME>ById(int Id)
        {
            return Context.<MODULE_NAME>.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert <MODULE_NAME> into database
        /// </summary>
        /// <param name="<MODULE_NAME>">Object infomation</param>
        public void Insert<MODULE_NAME>(<MODULE_NAME> <MODULE_NAME>)
        {
            Context.<MODULE_NAME>.Add(<MODULE_NAME>);
            Context.Entry(<MODULE_NAME>).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete <MODULE_NAME> with specific id
        /// </summary>
        /// <param name="Id"><MODULE_NAME> Id</param>
        public void Delete<MODULE_NAME>(int Id)
        {
            <MODULE_NAME> deleted<MODULE_NAME> = Get<MODULE_NAME>ById(Id);
            Context.<MODULE_NAME>.Remove(deleted<MODULE_NAME>);
            Context.Entry(deleted<MODULE_NAME>).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a <MODULE_NAME> with its Id and Update IsDeleted IF that <MODULE_NAME> has relationship with others
        /// </summary>
        /// <param name="<MODULE_NAME>Id">Id of <MODULE_NAME></param>
        public void Delete<MODULE_NAME>Rs(int Id)
        {
            <MODULE_NAME> delete<MODULE_NAME>Rs = Get<MODULE_NAME>ById(Id);
            delete<MODULE_NAME>Rs.IsDeleted = true;
            Update<MODULE_NAME>(delete<MODULE_NAME>Rs);
        }

        /// <summary>
        /// Update <MODULE_NAME> into database
        /// </summary>
        /// <param name="<MODULE_NAME>"><MODULE_NAME> object</param>
        public void Update<MODULE_NAME>(<MODULE_NAME> <MODULE_NAME>)
        {
            Context.Entry(<MODULE_NAME>).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
