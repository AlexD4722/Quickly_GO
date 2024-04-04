using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuicklyGo.Data.DTOs.Conversation;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Feature.Conversation.Requests.Command;
using QuicklyGo.Feature.Conversation.Requests.Query;
using QuicklyGo.Feature.UserConversations.Handlers.Queries;
using QuicklyGo.Feature.UserConversations.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class ConversationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConversationsController( IMediator mediator) {
            _mediator = mediator;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<Conversation>> CreateConversation([FromBody] ConversationInfoDTO conversation)
        {
            var command = new CreateConversationRequest(conversation);
            var response = await _mediator.Send(command);
            if(response.Success == true)
            {
                return CreatedAtAction(nameof(CreateConversation), conversation);
            }
            else
            {
                return BadRequest(response);
            }

        }

        //Get by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConversation(string id)
        {
            var command = new GetConversationInfoRequest(id);
            var response = await _mediator.Send(command);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        //Update 
        [HttpPut]
        public async Task<IActionResult> UpdateConversation(ConversationInfoDTO conversation)
        {
            var command = new UpdateConversationRequest(conversation);
            var response = await _mediator.Send(command);
            if (response != null)
            {
                return StatusCode(204);
            }
            else
            {
                return BadRequest(response);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversation(string id)
        {
            var command = new DeleteConversationRequest(id);
            var response = await _mediator.Send(command);
            if (response == true)
            {
                return StatusCode(202);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpGet("TestUserConveastion")]
        public async Task<IActionResult> UserConversationByUserId(string id)
        {
            var command = new GetConversationByIdUserRequest(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet("TestUserConveastionApi")]
        public async Task<IActionResult> GetConversationsByUserIdQuery(string userId)
        {
            var conversations = await _mediator.Send(new GetConversationsByUserIdQuery(userId));
            return Ok(conversations);
        }
        [HttpGet("GetConversationChatDetailByUserIdRequest")]
        public async Task<IActionResult> TestGetChatConversation(string userId)
        {
            var conversations = await _mediator.Send(new GetConversationChatDetailByUserIdRequest(userId));
            return Ok(conversations);
        }


    }

}
