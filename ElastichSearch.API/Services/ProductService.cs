using ElastichSearch.API.DTOs;
using ElastichSearch.API.Models;
using ElastichSearch.API.Repositories;
using System.Collections.Immutable;

namespace ElastichSearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto productCreateDto)
        {
            var response = await _productRepository.SaveAsync(productCreateDto.CreateProduct());

            if (response == null)
                return ResponseDto<ProductDto>.Fail(new List<string> { "Error occurred while saving" }, System.Net.HttpStatusCode.InternalServerError);

            return ResponseDto<ProductDto>.Success(response.CreateDto(), System.Net.HttpStatusCode.Created);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productListDto = new List<ProductDto>();


            productListDto = products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Stock,
                new ProductFeatureDto(x.Feature!.Width, x.Feature!.Height, x.Feature!.Color.ToString()))).ToList();

            return ResponseDto<List<ProductDto>>.Success(productListDto, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetById(string id)
        {

            var response = await _productRepository.GetById(id);
            if (response == null)
                return ResponseDto<ProductDto>.Fail("Product Not Found", System.Net.HttpStatusCode.NotFound);

            return ResponseDto<ProductDto>.Success(response.CreateDto(), System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var response = await _productRepository.UpdateAsync(productUpdateDto);

            if (!response)
                return ResponseDto<bool>.Fail("An error occurred while updating data", System.Net.HttpStatusCode.NotFound);

            return ResponseDto<bool>.Success(response, System.Net.HttpStatusCode.OK);
        }


        //example for logging and using Elastic results
        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var deleteResponse = await _productRepository.DeleteAsync(id);
            if (!deleteResponse.IsValid && deleteResponse.Result == Nest.Result.NotFound)
                return ResponseDto<bool>.Fail("Data not found", System.Net.HttpStatusCode.NotFound);

            if (!deleteResponse.IsValid)
            {
                _logger.LogError(deleteResponse.OriginalException, deleteResponse.ServerError.ToString());
                return ResponseDto<bool>.Fail("An error occurred while deleting data", System.Net.HttpStatusCode.NotFound);
            }

            return ResponseDto<bool>.Success(deleteResponse.IsValid, System.Net.HttpStatusCode.OK);
        }
    }
}
