using ElastichSearch.API.Models;
using Nest;

namespace ElastichSearch.API.Repositories
{
    public class ProductRepository
    {
        private readonly ElasticClient _elastichClient;


        public ProductRepository(ElasticClient elastichClient)
        {
            _elastichClient = elastichClient;
        }

        public async Task<Product?> SaveAsync(Product newProduct)
        {
            newProduct.Created = DateTime.Now;

            var response = await _elastichClient.IndexAsync(newProduct, x => x.Index("products").Id(Guid.NewGuid().ToString()));

            if (!response.IsValid) return null;

            newProduct.Id = response.Id;

            return newProduct;

        }
    }
}
