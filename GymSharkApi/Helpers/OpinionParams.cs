﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.Helpers
{
    public class OpinionParams: PaginationParams
    {
        public string ProductName { get; set; }
    }
}
