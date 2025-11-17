using Domain.Member.Events;
using Domain.Members.Entities;
using Domain.Members.Events;
using Marten.Events.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Projections
{
    public class MemberProjection : SingleStreamProjection<Member, Guid>
    {
        
        public Member Create(MemberRegisteredEvent e)
        {
            var view = new Member();
            view.Id = e.MemberId;
            view.FullName = e.FullName;
            view.Email = e.Email;
            view.IsActive = true;
            return view;
        }

        public void Apply(MemberSuspendedEvent e, Member view)
        {
            view.IsActive = false;
        }

        public void Apply(MemberActivatedEvent e, Member view)
        {
            view.IsActive = true;
        }
    }
}
