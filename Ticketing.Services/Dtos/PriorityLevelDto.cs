using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Enums;

namespace Ticketing.Services.Dtos
{
    public class PriorityLevelDto
    {
        public PriorityLevel Priority{ get; set; }
        public string DisplayName { get; set; }

    }
}
