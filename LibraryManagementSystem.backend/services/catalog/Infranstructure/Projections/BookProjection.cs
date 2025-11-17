using Domain.Books.Entities;
using Domain.Books.Events;
using Marten.Events.Aggregation;
using Marten.Events.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Projections
{
    public class BookProjection : SingleStreamProjection<Book, Guid>
    {
        public Book Create(BookRegisteredEvent e)
        {
            var view = new Book();
            view.Id = e.BookId;
            view.Title = e.Title;
            view.Author = e.Author;
            view.ISBN = e.ISBN;
            view.TotalQuantity = e.TotalQuantity;
            view.AvailableQuantity = e.TotalQuantity;
            view.IsRetired = false;
            return view;
        }


        public void Apply(BookUpdatedEvent e, Book view)
        {
            view.Title = e.Title;
            view.Author = e.Author;
            view.ISBN = e.ISBN;
            view.TotalQuantity=e.TotalQuantity;
            view.AvailableQuantity=e.AvailableQuantity;
        }

        public void Apply(BookRetiredEvent e, Book view)
        {
            view.IsRetired = true;
        }

      
        public void Apply(BookMadeAvailable e, Book view)
        {
            view.IsRetired = false;
        }

        public void Apply(BookBorrowedEvent e, Book view)
        {
            view.AvailableQuantity = view.AvailableQuantity -1;
        }

        public void Apply(BookReturnedEvent e, Book view)
        {
            view.AvailableQuantity = view.AvailableQuantity + 1;
        }
        public void Apply(BookLostEvent e, Book view)
        {
            view.TotalQuantity = view.TotalQuantity - 1;

        }
        public void Apply(BookFoundEvent e, Book view)
        {
            view.TotalQuantity = view.TotalQuantity + 1;

        }
        
    }
}
