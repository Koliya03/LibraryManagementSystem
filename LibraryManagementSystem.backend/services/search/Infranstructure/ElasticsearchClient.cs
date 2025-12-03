using Application.Interfaces;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure
{
    public class ElasticsearchClient : IElasticsearchClient
    {
        private readonly ElasticsearchSettings _settings;

        public ElasticsearchClient(IOptions<ElasticsearchSettings> settings)
        {
            _settings = settings.Value;
        }

        public IElasticClient CreateClient()
        {
            var connectionSettings = new ConnectionSettings(new Uri(_settings.Url));

            return new ElasticClient(connectionSettings);
        }
    }
}
