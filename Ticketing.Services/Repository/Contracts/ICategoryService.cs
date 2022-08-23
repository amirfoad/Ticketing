using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;
using Ticketing.Services.Dtos.CategoryDtos;

namespace Ticketing.Services.Repository.Contracts
{
    public interface ICategoryService
    {
        Task<OperationResult<List<CategoryDto>>> GetAll();
        Task<OperationResult<CategoryDto>> Get(int id);
        Task<OperationResult<CreateResultDto>> Create(CategoryCreateDto model);
        Task<OperationResult<bool>> Update(CategoryUpdateDto model);
        Task<OperationResult<bool>> Delete(int id);

        Task<bool> CategoryIsExist(int id);
    }
}
