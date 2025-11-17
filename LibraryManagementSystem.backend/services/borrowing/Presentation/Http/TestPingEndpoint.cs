using Messages.Borrowing;
using Wolverine;
using Wolverine.Http;

namespace Presentation.Http
{
    public class TestPingEndpoint
    {
        [WolverineGet("/api/test-ping/{name}")]
        public static async Task<IResult> TestPing(string name, IMessageContext bus)
        {
            Console.WriteLine($"🚀 Sending TestPingRequest to Catalog: {name}");

            Messages.Borrowing.TestPingEndpoint response = await bus.InvokeAsync<Messages.Borrowing.TestPingEndpoint>(
                new TestPingRequest(name)
            );

            Console.WriteLine($"⬅ Borrowing received response: {response.Reply}");

            return Results.Ok(response);
        }
    }
}
