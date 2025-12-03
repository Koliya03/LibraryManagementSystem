using Messages.Borrowing;
using Messages.Catalog.Events;
using Wolverine;
using Wolverine.Http;

namespace Presentation.Http
{

    public static class TestPingEndpoint
    {
        [WolverineGet("/test/send")]
        public static (IResult, OutgoingMessages) Send()
        {
            var outgoing = new OutgoingMessages();

            // This will be automatically published by Wolverine
            outgoing.Add(new TestPingFromCatalog("Hello from catalog!"));

            return (
                Results.Ok("Ping sent via OutgoingMessages!"),
                outgoing
            );
        }
    }
}
