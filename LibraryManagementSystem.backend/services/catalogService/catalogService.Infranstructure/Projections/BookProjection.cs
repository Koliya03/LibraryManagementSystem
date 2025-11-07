using Application.ReadModels;
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
    public class BookProjection : SingleStreamProjection<BookReadModel, Guid>
    {
        public BookReadModel Create(BookRegisteredEvent e)
        {
            var view = new BookReadModel();
            view.Id = e.BookId;
            view.Title = e.Title;
            view.Author = e.Author;
            view.ISBN = e.ISBN;
            view.TotalQuantity = e.TotalQuantity;
            view.AvailableQuantity = e.TotalQuantity;
            view.IsRetired = false;
            return view;
        }


        public void Apply(BookUpdatedEvent e, BookReadModel view)
        {
            view.Title = e.Title;
            view.Author = e.Author;
            view.ISBN = e.ISBN;
        }

        public void Apply(BookRetiredEvent e, BookReadModel view)
        {
            view.IsRetired = true;
        }

      
        public void Apply(BookMadeAvailable e, BookReadModel view)
        {
            view.IsRetired = false;
        }

        public void Apply(BookBorrowedEvent e, BookReadModel view)
        {
            view.AvailableQuantity -= e.Quantity;
        }

        public void Apply(BookReturnedEvent e, BookReadModel view)
        {
            view.AvailableQuantity += e.Quantity;
        }
    }
}
