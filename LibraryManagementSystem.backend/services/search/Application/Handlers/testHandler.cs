using Messages.Borrowing;
using Messages.Catalog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class testHandler
    {
        public void Handle(TestPingFromCatalog message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" search RECEIVED ping from catalog:");
            Console.ResetColor();

            Console.WriteLine(" CATALOG RECEIVED PING:");
            Console.WriteLine(message.Text);
        }
    }
    
}
