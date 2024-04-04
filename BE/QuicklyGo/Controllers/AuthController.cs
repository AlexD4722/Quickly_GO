using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuicklyGo.Data.DTOs.User;
using System.Security.Claims;
using QuicklyGo.Utils;
using QuicklyGo.Feature.User.Requests.Command;
using QuicklyGo.Data.DTOs.User.Validator;
using QuicklyGo.Feature.User.Requests.Queries;
using QuicklyGo.Reponses;
using System.Numerics;
using QuicklyGo.Unit;
using QuicklyGo.Data.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using QuicklyGo.Models;

namespace QuicklyGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto user)
        {
            // check if user + pass combination is valid
            var dbUser = await _mediator.Send(new ConfirmUserLoginRequest(user));
            if (dbUser != null)
            {

                // check if code verify is exist
                if (dbUser.Status != Models.UserStatus.Active)
                {
                    return BadRequest("Account is not active");
                }

                // create token and return it
                return Ok(new
                {
                    Token = TokenManager.CreateToken(dbUser.Id, dbUser.Role.ToString()),
                    RefreshToken = TokenManager.CreateRefreshToken(dbUser.Id)
                });
            }
            // return unauthorized if user + pass combination is invalid
            return Unauthorized();
        }
        [HttpPost("refreshToken")]
        public async Task<ActionResult> RefreshToken(string refreshToken)
        {
            // check if refresh token is valid
            var tokenValid = TokenManager.ValidateToken(refreshToken);
            if (tokenValid == false)
            {
                return Unauthorized();
            }
            
            // get user id from refresh token
            var userId = TokenManager.GetIdFromToken(refreshToken);

            // create new token and return it
            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            var tokenString = TokenManager.CreateToken(user.Id, user.Role.ToString());
            return Ok(new
            {
                Token = tokenString
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserDto user)
        {
            try
            {
                // check if user info already exists
                var duplicate = await _mediator
                    .Send(new CheckCreatingUserInfoRequest(user));
                if (duplicate.UsernameExists || duplicate.EmailExists || duplicate.PhoneNumberExists)
                {
                    return BadRequest(duplicate);
                }
                // create user
                var result = await _mediator.Send(new CreateUserCommand(user));
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return CreatedAtAction(nameof(Register), result);
            }
            catch (Exception ex)
            {
                // return error message
                return Problem(ex.Message);
            }
        }

        [HttpPost("verifyEmail")]
        public async Task<ActionResult> VerifyEmail(VerifyEmailDto data)
        {
            if (data.UserId == null)
            {
                return BadRequest("UserId is null");
            }
            if (data.VerifyCode == null)
            {
                return BadRequest("VerifyCode is null");
            }
            var result = await _mediator.Send(new ValidateEmailUserRequest(data.UserId, data.VerifyCode));
            if (result == false)
            {
                return BadRequest("Email authentication failed");
            }
            var response = await _mediator.Send(new UpdateStatusUserRequest(data.UserId, UserStatus.Active));
            return Ok("Success");
        }

        [Authorize]
        [HttpGet("userId")]
        public ActionResult<object> GetUserId()
        {
            return new { UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value };
        }
        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken(TokenDto tokenValue)
        {
            var resut = Auth.ValidateToken(tokenValue.tokenString);
            if (resut == null)
            {
                return Unauthorized();
            }
            return Ok(resut);
        }

    }
}
