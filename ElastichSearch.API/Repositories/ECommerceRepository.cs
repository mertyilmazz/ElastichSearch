using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models.ECommercoModel;
using System.Collections.Immutable;

namespace ElasticSearch.API.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _elastichClient;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient elastichClient)
        {
            _elastichClient = elastichClient;
        }

        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {

            //First way
            //var result = await _elastichClient.SearchAsync<ECommerce>(s=>s.Index(indexName).Query(q => q.Term(t => t.Field
            //("customer_first_name.keyword").Value(customerFirstName))));


            //Second way
            //var result = await _elastichClient.SearchAsync<ECommerce>(s => s.Index(indexName).
            //Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"), customerFirstName)));


            //Other way
            var termQuery = new TermQuery("customer_first_name.keyword") { Value = customerFirstName, CaseInsensitive=true };

            var result = await _elastichClient.SearchAsync<ECommerce>(s=>s.Index(indexName).Query(termQuery));

            foreach (var item in result.Hits)
            {
                item.Source!.Id = item.Id;
            }


            return result.Documents.ToImmutableList();
        }
    }
}
