using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

[assembly:WolverineModule]
namespace Application.Interfaces
{
    public interface IApplicationStore : IDocumentStore
    {
    }
}
