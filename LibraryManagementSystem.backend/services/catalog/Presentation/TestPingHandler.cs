using Messages.Borrowing;

namespace Presentation
{
    public class TestPingHandler
    {
        public void Handle(TestPing message)
        {
            Console.WriteLine(" CATALOG RECEIVED PING:");
            Console.WriteLine( message.Text);
        }
    }
}
