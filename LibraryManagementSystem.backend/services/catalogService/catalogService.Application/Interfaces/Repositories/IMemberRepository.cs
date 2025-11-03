using Domain.Books.Entities;
using Domain.Members.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMemberRepository
    {
        Task AddMemberAsync(Member member);
        Task<Member?> GetByIdAsync(Guid id);
        Task<List<Member>> GetAllAsync();
        Task UpdateMemberAsync(Member member);
    }
}
