using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Data;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Feature.User.Requests.Command;
using QuicklyGo.Feature.User.Requests.Queries;
using QuicklyGo.Models;
using QuicklyGo.Reponses.Exceptions;

namespace QuicklyGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuicklyGoDbContext _context;
        private readonly IMediator _mediator;

        public UsersController(QuicklyGoDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        // GET: api/Users
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetListInfoDetailUsersRequest());
            return Ok(users);
        }

        [HttpGet("FullInfo/{key}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<User>> GetInfoUserDetail(string key)
        {
            var user = await _mediator.Send(new GetInfoUserDetailRequest { KeyUser = key });
            if (user == null)
            {
                throw new NotFoundException(nameof(User), key);
            }
            return Ok(user);
        }
        // GET: api/Users/5
        [HttpGet("ViewInfoUser")]
        [Authorize]
        public async Task<ActionResult<User>> GetViewUserById()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _mediator.Send(new GetViewUserRequest { KeyUser = userId });
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<User>> PostUser([FromBody] CreateUserDto user)
        {
            var command = new CreateUserCommand(user);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
