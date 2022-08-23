using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.WebFrameWork;
using Ticketing.Services.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;
using Ticketing.Services.Repository.Contracts;

namespace Ticketing.WebApi.Controllers
{
    [Display(Name = "دسته بندی")]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Category")]
  
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return OperationResult(result);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryService.Get(id);
            return OperationResult(result);
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CategoryCreateDto model)
        {
            var result = await _categoryService.Create(model);
            return OperationResult(result);
        }
        [HttpPut("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(CategoryUpdateDto model)
        {
            var result = await _categoryService.Update(model);
            return OperationResult(result);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            return OperationResult(result);
        }


    }
}
