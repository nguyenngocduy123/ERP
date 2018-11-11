using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.RealEstate.Entities
{
    public class Project
    {
        public Project()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
        public string Investor { get; set; }
        public string Constructor { get; set; }
        public double? SiteArea { get; set; }
        public double? GrossFloorArea { get; set; }
        public Nullable<int> DensityOfBuilding { get; set; }
        public Nullable<System.DateTime> LaunchDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public Nullable<int> NumbersOfBlocks { get; set; }
        public Nullable<int> FloorHeight { get; set; }
        public Nullable<int> NumbersOfCondos { get; set; }
        public double? SmallestArea { get; set; }
        public double? BiggestArea { get; set; }
        public string ProjectType { get; set; }

    }
}
