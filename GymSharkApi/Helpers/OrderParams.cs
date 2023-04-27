using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Helpers
{
    public class OrderParams: PaginationParams
    {
        public string Predicate { get; set; }
        public int UserId { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
