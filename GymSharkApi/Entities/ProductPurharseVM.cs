using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Entities
{
    public class ProductPurharseVM: Product
    {
        public string Nonce { get; set; }
    }
}
