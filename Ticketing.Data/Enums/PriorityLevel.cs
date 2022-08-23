using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Data.Enums
{
    public enum PriorityLevel
    {
        [Display(Name = "کم")]
        Low = 0,
        [Display(Name = "معمولی")]
        Normal = 1,
        [Display(Name = "زیاد")]
        High = 2
    }
}
