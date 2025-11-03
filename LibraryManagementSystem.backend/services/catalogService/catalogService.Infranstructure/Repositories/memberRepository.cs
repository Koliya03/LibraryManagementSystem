using Application.Interfaces;
using Domain.Members.Entities;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Repositories
{
    public class memberRepository : IMemberRepository
    {
        private readonly IDocumentSession _session;

        public memberRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task AddMemberAsync(Member member)
        {
            _session.Store(member);
            await _session.SaveChangesAsync();
        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            return await _session.LoadAsync<Member>(id);
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return (await _session.Query<Member>().ToListAsync()).ToList();
        }

        public async Task UpdateMemberAsync(Member member)
        {
            _session.Store(member);
            await _session.SaveChangesAsync();
        }
    }
}
