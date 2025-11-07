using Application.ReadModels;
using Domain.Member.Events;
using Domain.Members.Events;
using Marten.Events.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Projections
{
    public class MemberProjection : SingleStreamProjection<MemberReadModel, Guid>
    {
        
        public MemberReadModel Create(MemberRegisteredEvent e)
        {
            var view = new MemberReadModel();
            view.Id = e.MemberId;
            view.FullName = e.FullName;
            view.Email = e.Email;
            view.IsActive = true;
            return view;
        }

        public void Apply(MemberSuspendedEvent e, MemberReadModel view)
        {
            view.IsActive = false;
        }

        public void Apply(MemberActivatedEvent e, MemberReadModel view)
        {
            view.IsActive = true;
        }
    }
}
