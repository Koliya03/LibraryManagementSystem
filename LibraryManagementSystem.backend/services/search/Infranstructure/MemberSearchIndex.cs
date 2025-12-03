using Application.Interfaces;
using Domain.Entities;
using ImTools;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure
{
    public class MemberSearchIndex : ISearchIndex<Member>
    {
        private readonly IElasticClient _client;
        private readonly string _indexName = "members_index";

        public MemberSearchIndex(IElasticClient client)
        {
            _client = client;
        }
        public async Task IndexAsync(Member document)
        {
            await _client.IndexAsync(document, i => i
                .Index(_indexName)
                .Id(document.Id.ToString())
            );
        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            var response = await _client.GetAsync<Member>(
                id.ToString(),
                g => g.Index(_indexName)
            );

            return response.Found ? response.Source : null;
        }

        public async Task<List<Member>> SearchAsync(string query)
        {
            var response = await _client.SearchAsync<Member>(s => s
                .Index(_indexName)
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query(query)
                        .Fields(f => f
                            .Field(p => p.FullName)
                            .Field(p => p.Email)
                        )
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );
            return response.Documents.ToList();
        }
        public async Task UpdateAsync(Member member)
        {
            var response = await _client.UpdateAsync<Member, Member>(
                 DocumentPath<Member>.Id(member.Id),
                u => u.Index(_indexName)
                      .Doc(member)
            );

            if (!response.IsValid)
            {
                Console.WriteLine("Elasticsearch Update Failed:");
                Console.WriteLine(response.DebugInformation);
            }
        }



        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync<Member>(id.ToString(), d => d.Index(_indexName));
        }
    }
}
