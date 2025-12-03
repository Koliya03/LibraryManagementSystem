using Messages.Borrowing;
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

            outgoing.Add(new TestPing("Hello from Borrowing!"));

            return (
                Results.Ok(new { Message = "Ping sent via OutgoingMessages!" }),
                outgoing
            );
        }
    }



}
