using Application.Commands.Books;
using Application.DTO;
using Application.Queries.Book;
using Application.Queries.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Presentation.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly IMessageBus _bus;

        public BooksController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookResponseDto>>> GetAllBooks()
        {
            List<BookResponseDto> result = await _bus.InvokeAsync<List<BookResponseDto>>(new GetAllBooksQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookResponseDto>> GetBookById(Guid id)
        {
            BookResponseDto result = await _bus.InvokeAsync<BookResponseDto>(new GetBookByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("{id:guid}/availability")]
        public async Task<ActionResult<AvailabilityDto>> GetAvailability(Guid id)
        {
            AvailabilityDto result = await _bus.InvokeAsync<AvailabilityDto>(new GetBookAvailabilityQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBook([FromBody] BookRequestDto dto)
        {
            await _bus.InvokeAsync(new RegisterBookCommand(dto.Title, dto.Author, dto.ISBN, dto.TotalQuantity));
            return Ok("Book registered successfully.");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookRequestDto dto)
        {
            await _bus.InvokeAsync(new UpdateBookCommand(id, dto.Title, dto.Author, dto.ISBN,dto.TotalQuantity,dto.AvailableQuantity));
            return Ok("Book updated successfully.");
        }

        [HttpPost("{id:guid}/retire")]
        public async Task<IActionResult> RetireBook(Guid id)
        {
            await _bus.InvokeAsync(new RetireBookCommand(id));
            return Ok("Book retired successfully.");
        }

        [HttpPost("{id:guid}/make-available")]
        public async Task<IActionResult> MakeAvailable(Guid id)
        {
            await _bus.InvokeAsync(new MakeBookAvailableCommand(id));
            return Ok("Book made available.");
        }

        public class QuantityBody { public int Quantity { get; set; } }

        [HttpPost("{id:guid}/borrow")]
        public async Task<IActionResult> Borrow(Guid id, [FromBody] QuantityBody body)
        {
            await _bus.InvokeAsync(new BorrowBooksCommand(id, body.Quantity));
            return Ok("Borrow recorded.");
        }

        
    }
}
    
    
