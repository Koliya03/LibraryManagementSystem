using Messages.Catalog.Events;
using Messages.Catalog.Events.Books;

namespace Presentation
{
    public class consumertestHandler
    {
        public void Handle(BookRegisteredMessage message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" search RECEIVED book from catalog:");
            Console.ResetColor();

            Console.WriteLine(" CATALOG RECEIVED PING:");
            Console.WriteLine(message.Title);
        }

        //public void Handle(TestPingFromCatalog message)
        //{
        //    Console.BackgroundColor = ConsoleColor.Red;
        //    Console.WriteLine(" borrowed RECEIVED ping from catalog:");
        //    Console.ResetColor();

        //    Console.WriteLine(" CATALOG RECEIVED PING:");
        //    Console.WriteLine(message.Text);
        //}
    }
}
