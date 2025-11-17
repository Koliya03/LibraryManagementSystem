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
    }
}

