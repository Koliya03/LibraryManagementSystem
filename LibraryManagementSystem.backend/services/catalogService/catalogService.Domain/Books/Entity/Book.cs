using Domain.Books.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Books.Entities
{
    public class Book
    {
        public Guid Id { get;private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int TotalQuantity { get; private set; }
        public int AvailableQuantity { get; private set; }
        public bool IsRetired { get; private set; }

        public Book() { }

        public static BookRegisteredEvent Register(Guid id, string title, string author, string isbn, int totalQuantity)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new Exception("Title is required.");
            if (string.IsNullOrWhiteSpace(author)) throw new Exception("Author is required.");
            if (string.IsNullOrWhiteSpace(isbn)) throw new Exception("ISBN is required.");
           

            return new BookRegisteredEvent(id, title, author, isbn, totalQuantity);
        }
        public BookUpdatedEvent UpdateInfo(string title, string author, string isbn)
        {
            return new BookUpdatedEvent(Id, title, author, isbn);
        }

        public BookRetiredEvent Retire()
        {
            if (IsRetired)
            {
                throw new Exception("book is already retied.");
            }
             return new BookRetiredEvent(Id);
        }

        public BookMadeAvailable MakeAvailable()
        {
            if (!IsRetired) throw new Exception("Book is already available.");
            if (TotalQuantity <= 0) throw new Exception("Cannot make available with zero total quantity.");
            return new BookMadeAvailable(Id);
        }

        public BookBorrowedEvent Borrow(int quantity)
        {
            if (IsRetired) throw new Exception("Book is retired.");
            if (quantity <= 0) throw new Exception("Quantity must be > 0.");
            if (AvailableQuantity < quantity)
                throw new Exception("Not enough copies available.");
            return new BookBorrowedEvent(Id, quantity);
        }

        public BookReturnedEvent Return(int quantity)
        {
            if (quantity <= 0) throw new Exception("Quantity must be > 0.");
            var borrowed = TotalQuantity - AvailableQuantity;
            if (quantity > borrowed)
                throw new Exception("Return exceeds borrowed copies.");
            return new BookReturnedEvent(Id, quantity);
        }
        public void Apply(BookRegisteredEvent e)
        {
            Id = e.BookId;
            Title = e.Title;
            Author = e.Author;
            ISBN = e.ISBN;
            TotalQuantity = e.TotalQuantity;
            AvailableQuantity = e.TotalQuantity;
            IsRetired = false;
        }

        public void Apply(BookUpdatedEvent e)
        {
            Title = e.Title;
            Author = e.Author;
            ISBN = e.ISBN;
        }

        public void Apply(BookRetiredEvent e)
        {
            IsRetired = true;
        }

        public void Apply(BookMadeAvailable e)
        {
            IsRetired = false;
        }

        public void Apply(BookBorrowedEvent e)
        {
            AvailableQuantity -= e.Quantity;
        }

        public void Apply(BookReturnedEvent e)
        {
            AvailableQuantity += e.Quantity;
        }


    }



}
