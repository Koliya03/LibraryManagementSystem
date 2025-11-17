using Marten.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.Attributes;

namespace Application.Commands.Books
{
    public record RegisterBookCommand(
      Guid BookId,
      string Title,
      string Author,
      string ISBN,
      int TotalQuantity
  );
    //{
    //    public RegisterBookCommand(string title, string author, string isbn, int totalQuantity)
    //        : this(Guid.NewGuid(), title, author, isbn, totalQuantity)
    //    {
    //    }
    //}
}
