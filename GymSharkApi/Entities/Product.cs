using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string AboutCompany { get; set; }
        public string SpecifiedFor { get; set; }
        public string PhotoUrl { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<ProductOrder> OrderedByUsers { get; set; }
        public ICollection<Opinion> MessagesReceived { get; set; }
    }
}
