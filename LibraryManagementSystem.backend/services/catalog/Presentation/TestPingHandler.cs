using Messages.Borrowing;

namespace Presentation
{
    public class TestPingHandler
    {
        public void Handle(TestPing message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" CATALOG RECEIVED PING:");
            Console.ResetColor();
           
            Console.WriteLine(" CATALOG RECEIVED PING:");
            Console.WriteLine( message.Text);
        }
    }
}
