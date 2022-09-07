using GymSharkAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Entities
{
    public class ProductOrder
    {
        public AppUser SourceUser { get; set; }
        public int SourceUsertId { get; set; }
        public Product OrderedProduct { get; set; }
        public int OrderedProductId { get; set; }
    }
}
