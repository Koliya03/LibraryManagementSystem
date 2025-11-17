using Messages.Borrowing;
using Wolverine.Attributes;

namespace Presentation
{
    public class TestPingRequestHandler
    {
        [MessageTimeout(1)]
        public TestPingEndpoint Handle(TestPingRequest message)
        {
            Console.WriteLine($"Catalog received TestPingRequest: {message.Name}");
            return new TestPingEndpoint($"Hello {message.Name}");
        }
    }
}
