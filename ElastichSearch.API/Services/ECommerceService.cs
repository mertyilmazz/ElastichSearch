using ElastichSearch.API.DTOs;
using ElastichSearch.API.Repositories;
using ElastichSearch.API.Services;
using ElasticSearch.API.Models.ECommercoModel;
using ElasticSearch.API.Repositories;
using System.Collections.Immutable;

namespace ElasticSearch.API.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _ecommerceRepository;
        private readonly ILogger<ProductService> _logger;

        public ECommerceService(ECommerceRepository ecommerceRepository, ILogger<ProductService> logger)
        {
            _ecommerceRepository = ecommerceRepository;
            _logger = logger;
        }


        public async Task<ImmutableList<ECommerce>> TermQueryByCustomerFullName(string customerFullName)
        {
            var response = await _ecommerceRepository.TermQuery(customerFullName);
            return response;
        
        }
    }
}
