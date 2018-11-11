using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Erp.Domain.Repositories
{
    public class SettingRepository: GenericRepository<ErpDbContext, Setting>, ISettingRepository
    {
        public SettingRepository(ErpDbContext context)
            : base(context)
        {

        }


        public IEnumerable<Setting> GetAlls()
        {
            return Context.Setting.AsEnumerable();
        }

        public Setting GetById(int Id)
        {
            return Context.Setting.SingleOrDefault(m => m.Id == Id);
        }

        public void Insert(Setting setting)
        {
            Context.Setting.Add(setting);

            Context.Entry(setting).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void Delete(int Id)
        {
            Setting setting = GetById(Id);
            Context.Setting.Remove(setting);

            Context.Entry(setting).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void Update(Setting setting)
        {
            Context.Entry(setting).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public Setting GetSettingByKey(string key)
        {
            var setting = Context.Setting.FirstOrDefault(s => s.Key == key);
            if (setting != null)
            {
                return setting;
            }

            return null;
        }
    }
}
