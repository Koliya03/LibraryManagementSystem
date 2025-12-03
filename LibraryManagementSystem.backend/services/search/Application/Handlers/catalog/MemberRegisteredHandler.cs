using Application.Interfaces;
using Domain.Entities;
using Messages.Catalog.Events.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.catalog
{
    public static class MemberRegisteredHandler
    {
        public static Task Handle(MemberRegisteredMessage message, ISearchIndex<Member> _members)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" registered a member:");
            Console.ResetColor();

            var doc = new Member
            {
                Id = message.MemberId,
                FullName = message.FullName,
                Email = message.Email,
                IsActive = message.IsActive
            };

            return _members.IndexAsync(doc);
        }
    }
}
