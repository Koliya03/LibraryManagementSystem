using Messages.Borrowing;
using Messages.Catalog.Events;
using Messages.Catalog.Events.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class TestPingHandler
    {
        public void Handle(BookRegisteredMessage message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" search RECEIVED book from catalog:");
            Console.ResetColor();

            Console.WriteLine(" CATALOG RECEIVED PING:");
            Console.WriteLine(message.Title);
        }
    }
}
