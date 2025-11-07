using Domain.Member.Events;
using Domain.Members.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Members.Entities
{
    public class Member
    {
        public Guid Id {  get; set; }   
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive {  get; set; }

        public Member() { }


        public static MemberRegisteredEvent Register(Guid id, string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new Exception("Full name is required.");
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("Email is required.");

            return new MemberRegisteredEvent(id, fullName, email);
        }

        public MemberSuspendedEvent Suspend()
        {
            if (!IsActive) throw new Exception("Member is already suspended.");
            return new MemberSuspendedEvent(Id);
        }

        public MemberActivatedEvent Activate()
        {
            if (IsActive) throw new Exception("Member is already active.");
            return new MemberActivatedEvent(Id);
        }

        public void Apply(MemberRegisteredEvent e)
        {
            Id = e.MemberId;
            FullName = e.FullName;
            Email = e.Email;
            IsActive = true;
        }

        public void Apply(MemberSuspendedEvent _)
        {
            IsActive = false;
        }

        public void Apply(MemberActivatedEvent _)
        {
            IsActive = true;
        }
    }
}

