using Elastic.Clients.Elasticsearch;
using ElastichSearch.API.DTOs;
using ElastichSearch.API.Models;
using System.Collections.Immutable;

namespace ElastichSearch.API.Repositories
{
    public class ProductRepository
    {
        private readonly ElasticsearchClient _elastichClient;
        private const string indexName = "products";


        public ProductRepository(ElasticsearchClient elastichClient)
        {
            _elastichClient = elastichClient;
        }

        public async Task<Product?> SaveAsync(Product newProduct)
        {
            newProduct.Created = DateTime.Now;

            var response = await _elastichClient.IndexAsync(newProduct, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));

            if (!response.IsValidResponse) return null;

            newProduct.Id = response.Id;

            return newProduct;

        }

        public async Task<ImmutableList<Product>> GetAllAsync()
        {
            var result = await _elastichClient.SearchAsync<Product>(s =>
            s.Index(indexName)
            .Query(q => q.MatchAll()));

            foreach (var item in result.Hits)
            {
                item.Source.Id = item.Id;
            }

            return result.Documents.ToImmutableList();
        }

        public async Task<Product> GetById(string id)
        {
            var response = await _elastichClient.GetAsync<Product>(id, x => x.Index(indexName));
            response.Source.Id = response.Id;
            return response.Source;
        }


        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            //var response = await _elastichClient.UpdateAsync<Product, ProductUpdateDto>(productUpdateDto.id,
            //    x => x.Index(indexName).Doc(productUpdateDto));

            var response = await _elastichClient.UpdateAsync<Product, ProductUpdateDto>(indexName, productUpdateDto.id, x => x.Doc(productUpdateDto));
            return response.IsValidResponse;
        }


        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var response = await _elastichClient.DeleteAsync<Product>(id, x => x.Index(indexName));
            return response;
        }
    }
}
