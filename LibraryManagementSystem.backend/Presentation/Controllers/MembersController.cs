using Application.Commands.Members;
using Application.DTO;
using Application.Queries.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Presentation.Controllers
{
    [Route("api/catalog/members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMessageBus _bus;

        public MembersController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<List<MemberResponseDto>>> GetAll()
        {
            List<MemberResponseDto> result = await _bus.InvokeAsync<List<MemberResponseDto>>(new GetAllMembersQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MemberResponseDto>> GetById(Guid id)
        {
            MemberResponseDto result = await _bus.InvokeAsync<MemberResponseDto>(new GetMemberByIdQuery(id));
            if (result == null) return BadRequest("member can not be found");
            return Ok(result);
        }

        [HttpGet("{id:guid}/status")]
        public async Task<ActionResult<MemberStatusDto>> GetStatus(Guid id)
        {
            MemberStatusDto result = await _bus.InvokeAsync<MemberStatusDto>(new GetMemberActiveStatusQuery(id));
            if (result == null) return NotFound("member can not be found");
            return Ok(result);
        }

        [HttpGet("{id:guid}/active-status")]
        public async Task<ActionResult<MemberStatusDto>> GetActiveStatus(Guid id)
        {
            MemberStatusDto result = await _bus.InvokeAsync<MemberStatusDto>(new GetMemberActiveStatusQuery(id));
            if (result == null) return NotFound("member can not be found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] MemberRequestDto dto)
        {
            await _bus.InvokeAsync(new RegisterMemberCommand(dto.FullName, dto.Email));
            return Ok("Member registered successfully.");
        }

        [HttpPost("{id:guid}/suspend")]
        public async Task<IActionResult> Suspend(Guid id)
        {
            await _bus.InvokeAsync(new SuspendMemberCommand(id));
            return Ok("Member suspended.");
        }

        [HttpPost("{id:guid}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _bus.InvokeAsync(new ActivateMemberCommand(id));
            return Ok("Member activated.");
        }
    }
}
