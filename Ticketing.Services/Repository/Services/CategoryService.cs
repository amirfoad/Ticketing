using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Data.Persistence;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;
using Ticketing.Services.Dtos.CategoryDtos;
using Ticketing.Services.Repository.Contracts;
using Ticketing.Services.UnitOfWork;

namespace Ticketing.Services.Repository.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork<TicketingDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork<TicketingDbContext> unitOfWork
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<CreateResultDto>> Create(CategoryCreateDto model)
        {
            var categoryRepository = _unitOfWork.GetRepository<Category>();
            if (model.ParentId != null && model.ParentId != 0)
            {
                var isParentCategoryExist = await CategoryIsExist(model.ParentId.Value);

                if (!isParentCategoryExist)
                    return OperationResult<CreateResultDto>.NotFoundResult("دسته بندی پدر مورد نظر پیدا نشد.");
            }
            else
            {
                model.ParentId = null;
            }



            var categoryToAdd = _mapper.Map<Category>(model);


            await categoryRepository.AddAsync(categoryToAdd);
            await _unitOfWork.Commit();

            return OperationResult<CreateResultDto>.SuccessResult(new CreateResultDto { Id = categoryToAdd.Id });
        }

        public async Task<bool> CategoryIsExist(int id)
        {
            return await _unitOfWork.GetRepository<Category>().Query().AnyAsync(c => c.Id == id);
        }

        public async Task<OperationResult<bool>> Delete(int id)
        {
            var categoryRepository = await _unitOfWork.GetRepository<Category>().FindAsync(c => c.Id == id);
            if (categoryRepository is null)
                return OperationResult<bool>.NotFoundResult("دسته بندی مورد نظر پیدا نشد.");

            if (categoryRepository.ParentCategory is not null)
                return OperationResult<bool>.NotFoundResult("دسته بندی مورد نظر دارای زیرگروه میباشد.");



            await _unitOfWork.GetRepository<Category>().DeleteAsync(id);
            await _unitOfWork.Commit();

            return OperationResult<bool>.SuccessResult(true);
        }

        public async Task<OperationResult<CategoryDto>> Get(int id)
        {
            var category = await _unitOfWork.GetRepository<Category>().FindAsync(c => c.Id == id);
            if (category is null)
                return OperationResult<CategoryDto>.NotFoundResult("دسته بندی مورد نظر پیدا نشد.");

            return OperationResult<CategoryDto>.SuccessResult(_mapper.Map<CategoryDto>(category));
        }

        public async Task<OperationResult<List<CategoryDto>>> GetAll()
        {
            var result = await _mapper.ProjectTo<CategoryDto>(_unitOfWork.GetRepository<Category>().Query().AsNoTracking()).ToListAsync();
            return OperationResult<List<CategoryDto>>.SuccessResult(result);
        }

        public async Task<OperationResult<bool>> Update(CategoryUpdateDto model)
        {
            var categoryIsExist = await CategoryIsExist(model.Id);
            if (!categoryIsExist)
                return OperationResult<bool>.NotFoundResult("دسته بندی مورد نظر پیدا نشد.");
            if (model.ParentId != null && model.ParentId != 0)
            {
                var parentCategoryIsExist = await CategoryIsExist(model.ParentId.Value);
                if (!parentCategoryIsExist)
                    return OperationResult<bool>.NotFoundResult("دسته بندی پدر مورد نظر پیدا نشد.");
            }

            var categoryToUpdate = _mapper.Map<Category>(model);
            await _unitOfWork.GetRepository<Category>().UpdateAsync(categoryToUpdate);
            await _unitOfWork.Commit();

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
