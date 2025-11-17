using Domain.Entities;
using Domain.Events;
using Marten.Events.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infranstructure.Projections
{
    public class BorrowProjection : SingleStreamProjection<BorrowRecord, Guid>
    {
        public BorrowRecord Create(BorrowRecordCreatedEvent e)
        {
            var view = new BorrowRecord();
            view.Id = e.BorrowId;
            view.MemberId = e.MemberId;
            view.BookId = e.BookId;
            view.BorrowDate = e.BorrowDate;
            view.DueDate = e.DueDate;
            view.IsReturned = false;
            view.IsLost = false;
            view.LateFee = 0;
            return view;
        }
        public void Apply(BookReturnedEvent e, BorrowRecord view)
        {
            view.ReturnDate = e.ReturnDate;
            view.IsReturned = true;
            view.LateFee = e.LateFee;
        }
        public void Apply(BookMarkedLostEvent e, BorrowRecord view)
        {
            view.IsLost = true;
        }
        public void Apply(LateFeeIssuedEvent e, BorrowRecord view)
        {
            view.LateFee = e.FeeAmount;
        }
        public void Apply(BookMarkedFoundEvent e, BorrowRecord view)
        {
            view.IsLost = false;
        }
    }
}
