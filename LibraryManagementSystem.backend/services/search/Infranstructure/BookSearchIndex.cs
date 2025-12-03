using Application.Interfaces;
using Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure
{
    public class BookSearchIndex : ISearchIndex<Book>
    {
        private readonly IElasticClient _client;
        private readonly string _indexName = "books_index";

        public BookSearchIndex(IElasticClient client)
        {
            _client = client;
        }

        public async Task IndexAsync(Book document)
        {
            await _client.IndexAsync(document, i => i
                .Index(_indexName)
                .Id(document.Id.ToString()) 
            );
        }

        //public async Task<Book?> GetByIdAsync(Guid id)
        //{
        //    var response = await _client.SearchAsync<Book>(s => s
        //        .Index(_indexName)
        //        .Query(q => q
        //            .Term(t => t.Field(f => f.Id).Value(id))   
        //        )
        //    );

        //    return response.Documents.FirstOrDefault();
        //}

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            var response = await _client.GetAsync<Book>(id.ToString(), g => g.Index(_indexName));

            return response.Found ? response.Source : null;
        }

        public async Task<List<Book>> SearchAsync(string query)
        {
            var response = await _client.SearchAsync<Book>(s => s
                .Index(_indexName)
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query(query)
                        .Fields(f => f
                            .Field(p => p.Title)
                            .Field(p => p.Author)
                            .Field(p => p.ISBN)
                        )
                        .Fuzziness(Fuzziness.Auto)   
                    )
                )
            );


            return response.Documents.ToList();
        }

        //public async Task UpdateAsync(Book book)
        //{
        //    var response = await _client.UpdateAsync<Book, Book>(
        //        $"{book.BookId}",
        //        u => u.Index(_indexName)
        //              .Doc(book)
        //    );
        //}
        public async Task UpdateAsync(Book book)
        {
            var response = await _client.UpdateAsync<Book, Book>(
                DocumentPath<Book>.Id(book.Id),    
                u => u.Index("books_index")
                      .Doc(book)                  
            );
            if (!response.IsValid)
                Console.WriteLine(response.DebugInformation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync<Book>(id.ToString(), d => d.Index(_indexName));
        }
    }
}
