using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Services.Profiles;

namespace Ticketing.Services.Dtos.CategoryDtos
{
    public class CategoryCreateDto:ICreateMapper<Category>
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }

    }
}
