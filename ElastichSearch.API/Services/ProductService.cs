using ElastichSearch.API.DTOs;
using ElastichSearch.API.Repositories;

namespace ElastichSearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto productCreateDto)
        {
            var response = await _productRepository.SaveAsync(productCreateDto.CreateProduct());

            if (response == null)            
                return ResponseDto<ProductDto>.Fail(new List<string> { "Error occurred while saving" }, System.Net.HttpStatusCode.InternalServerError);            

            return ResponseDto<ProductDto>.Success(response.CreateDto(), System.Net.HttpStatusCode.Created);
        }
    }
}
