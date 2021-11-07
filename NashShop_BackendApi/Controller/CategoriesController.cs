using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            var categories = await _categoryService.GetById(categoryId);
            return Ok(categories);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request) { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isSucceed = await _categoryService.Create(request);
            if (!isSucceed)
                return BadRequest();
            return Ok();
        }
        [HttpPut("{categoryId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = categoryId;
            var result = await _categoryService.Update(request);
            if (!result)
                return BadRequest();
            return Ok(result);

        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId);
            if (!result)
                return BadRequest();
            return Ok(result);

        }
    }
}
