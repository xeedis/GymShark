using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSharkApi.DTOs
{
    public class CreateMessageDto
    {
        public string RecipientName { get; set; }
        public string Content { get; set; }
    }
}
