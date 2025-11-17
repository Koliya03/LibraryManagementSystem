using Messages.Borrowing;
using Wolverine;
using Wolverine.Http;

namespace Presentation.Http
{
    //public class TestPingEndpoint
    //{
    //    [WolverineGet("/api/test-ping/{name}")]
    //    public static async Task<IResult> TestPing(string name, IMessageContext bus)
    //    {
    //        Console.WriteLine($"🚀 Sending TestPingRequest to Catalog: {name}");

    //        Messages.Borrowing.TestPingEndpoint response = await bus.InvokeAsync<Messages.Borrowing.TestPingEndpoint>(
    //            new TestPingRequest(name)
    //        );

    //        Console.WriteLine($"⬅ Borrowing received response: {response.Reply}");

    //        return Results.Ok(response);
    //    }
    //}
    public static class TestPingEndpoint
    {
        [WolverineGet("/test/send")]
        public static (IResult, OutgoingMessages) Send()
        {
            var outgoing = new OutgoingMessages();

            // This will be automatically published by Wolverine
            outgoing.Add(new TestPing("Hello from Borrowing!"));

            return (
                Results.Ok("Ping sent via OutgoingMessages!"),
                outgoing
            );
        }
    }



}
