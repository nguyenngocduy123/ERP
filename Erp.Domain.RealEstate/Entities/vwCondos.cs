using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.RealEstate.Entities
{
    public class vwCondos
    {
        public vwCondos()
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

        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> FloorId { get; set; }
        public string Code { get; set; }
        public double? Area { get; set; }
        public double? Price { get; set; }
        public Nullable<int> NumbersOfLivingRoom { get; set; }
        public Nullable<int> NumbersOfBedRoom { get; set; }
        public Nullable<int> NumbersOfKitchenRoom { get; set; }
        public Nullable<int> NumbersOfToilet { get; set; }
        public Nullable<int> NumbersOfBalcony { get; set; }
        public string Orientation { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string ProjectName { get; set; }
        public string BlockName { get; set; }
        public string  FloorName { get; set; }
    }
}
