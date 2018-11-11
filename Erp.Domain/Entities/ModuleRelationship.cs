using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class ModuleRelationship
    {
        public ModuleRelationship()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string First_ModuleName { get; set; }
        public string First_MetadataFieldName { get; set; }
        public string Second_ModuleName { get; set; }
        public string Second_ModuleName_Alias { get; set; }
        public string Second_MetadataFieldName { get; set; }        
    }
}
