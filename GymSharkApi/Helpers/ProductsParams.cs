using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Helpers
{
    public class ProductsParams:PaginationParams
    {
        public string Category { get; set; }
        public string CurrentName { get; set; }

        public int minPrice { get; set; }
        public int maxPrice { get; set; }
    }
}
