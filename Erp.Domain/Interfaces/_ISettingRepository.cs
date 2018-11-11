using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Interfaces
{
    public interface ISettingRepository
    {
        IEnumerable<Setting> GetAlls();
        Setting GetById(int Id);
        void Insert(Setting setting);
        void Delete(int Id);
        void Update(Setting setting);
        Setting GetSettingByKey(string key);
    }
}
