

using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElastichSearch.API.Extensions
{
    public static class ElasticsearchExtension
    {
        public static void AddElasticClient(this IServiceCollection services, IConfiguration configuration)
        {
            string userName = configuration.GetSection("Elastic")["Username"]!;
            string password = configuration.GetSection("Elastic")["Pasword"]!;
            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!))
                .Authentication(new BasicAuthentication(userName!, password!));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
