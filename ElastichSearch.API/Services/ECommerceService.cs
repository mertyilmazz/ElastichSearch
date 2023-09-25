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

        public async Task<ImmutableList<ECommerce>> TermsQueryByCustomerFullName(List<string> customerFirstName)
        {
            var response = await _ecommerceRepository.TermsQuery(customerFirstName);
            return response;

        }

        public async Task<ImmutableList<ECommerce>> PrefixQuery(string customerFullName)
        {
            var response = await _ecommerceRepository.PrefixQuery(customerFullName);
            return response;

        }

        public async Task<ImmutableList<ECommerce>> RangeQuery(double fromPrice, double toPrice)
        {
            var response = await _ecommerceRepository.RangeQuery(fromPrice,toPrice);
            return response;

        }
    }
}
