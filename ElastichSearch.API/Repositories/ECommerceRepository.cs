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
            var termQuery = new TermQuery("customer_first_name.keyword") { Value = customerFirstName, CaseInsensitive = true };

            var result = await _elastichClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery));

            foreach (var item in result.Hits)
            {
                item.Source!.Id = item.Id;
            }


            return result.Documents.ToImmutableList();
        }


        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameLIst)
        {
            List<FieldValue> terms = new List<FieldValue>();
            customerFirstNameLIst.ForEach(x =>
            {
                terms.Add(x);
            });


            // first way 
            //var termsQuery = new TermsQuery()
            //{
            //    Field = "customer_first.name.keyword",
            //    Terms = new TermsQueryField(terms.AsReadOnly())
            //};

            //var result = await _elastichClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termsQuery));


            //second way 
            var result = await _elastichClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q
            .Terms(t => t
            .Field(f => f.
            CustomerFirstName.Suffix("keyword")).
            Terms(new TermsQueryField(terms.AsReadOnly())))));

            return result.Documents.ToImmutableList();
        }


        public async Task<ImmutableList<ECommerce>> PrefixQuery(string customerName)
        {
            var result = await _elastichClient.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(150)
            .Query(q => q
            .Prefix(p => p
            .Field(f => f.CustomerFullName.Suffix("keyword"))
            .Value(customerName))));


            return result.Documents.ToImmutableList();
        }


        public async Task<ImmutableList<ECommerce>> RangeQuery(double fromPrice, double toPrice)
        {
            var result = await _elastichClient.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Range(r => r.NumberRange(nr => nr.
             Field(f => f.TaxFullTotalPrice)
            .Gte(fromPrice).Lte(toPrice)))));
            //.Field(f => f.CustomerFullName.Suffix("keyword"))
            //.Value(customerName))));


            return result.Documents.ToImmutableList();
        }
    }
}
