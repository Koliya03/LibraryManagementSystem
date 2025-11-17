using Messages.Borrowing;

namespace Presentation
{
    public class TestPingRequestHandler
    {
        public TestPingEndpoint Handle(TestPingRequest message)
        {
            Console.WriteLine($"Catalog received TestPingRequest: {message.Name}");
            return new TestPingEndpoint($"Hello {message.Name}");
        }
    }
}
