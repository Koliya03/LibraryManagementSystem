using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Messages.Catalog.Events
{

    [Topic("catalog.test")]
    public record TestPingFromCatalog(string Text);
}
