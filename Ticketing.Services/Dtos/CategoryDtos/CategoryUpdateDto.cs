using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Services.Profiles;

namespace Ticketing.Services.Dtos.CategoryDtos
{
    public class CategoryUpdateDto:ICreateMapper<Category>
    {
        [Required(ErrorMessage = "لطفا آیدی را وارد کنید")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        public string Title { get; set; }
        public int? ParentId { get; set; }

    }
}
