using ElastichSearch.API.DTOs;
using ElastichSearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElastichSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {

        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            var result = await _productService.SaveAsync(request);
            return CreateActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(ProductUpdateDto req)
        {
            var result = await _productService.UpdateAsync(req);
            return CreateActionResult(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.DeleteAsync(id);
            return CreateActionResult(result);
        }
    }
}
