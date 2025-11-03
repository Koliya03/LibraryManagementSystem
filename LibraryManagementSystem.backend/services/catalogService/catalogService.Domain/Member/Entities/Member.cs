using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Member.Entities
{
    public class Member
    {
        public Guid id {  get; set; }   
        public string fullName { get; set; }
        public string email { get; set; }
        public bool isActive {  get; set; }

    }
}
