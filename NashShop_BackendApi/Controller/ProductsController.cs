using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NashShop_BackendApi.Interfaces;
using NashShop_BackendApi.Services;
using NashShop_ViewModel;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var users = await _productService.GetAll();
            return Ok(users);

        }
        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }
        [HttpGet("paging/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCategoryId(int categoryId, [FromQuery] PagingRequest request)
        {
            var products = await _productService.GetByCategoryId(request, categoryId);
            return Ok(products);
        }
        [HttpGet("featured/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeaturedProducts(int take)
        {
            var products = await _productService.GetFeaturedProducts(take);
            return Ok(products);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId < 0)
                return BadRequest();
            var product = await _productService.GetById(productId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }
        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int productId)
        {
            var result = await _productService.GetById(productId);
            if (result == null)
                return BadRequest();
            return Ok(result);

        }
        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var result = await _productService.Update(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);

        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.Delete(productId);
            if (result == 0)
                return BadRequest();
            return Ok(result);

        }

        //Images
        [HttpPost("{productId}/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateImage([FromRoute] int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage([FromRoute] int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }
        [HttpGet("{productId}/images/{imageId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _productService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }
        [HttpPost("rating")]
        public async Task<IActionResult> CreateRating([FromBody] ProductRatingRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = User.FindFirst("UserId")?.Value;
            request.UserId = new Guid(id);
            var rating = await _productService.AddRating(request);
            if (!rating)
                return BadRequest();
            return Ok();
        }

        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetProductImages(int productId)
        {
            var images = await _productService.GetProductImages(productId);
            if (images == null)
                return BadRequest("Cannot find product");
            return Ok(images);
        }
    }
}
