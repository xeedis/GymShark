using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.DTOs
{
    public class ItemUpdateDto
    {
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
    }
}
