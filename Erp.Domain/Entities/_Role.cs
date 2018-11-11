using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class Role
    {
        public Role()
        {
            //this.Users = new List<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}
