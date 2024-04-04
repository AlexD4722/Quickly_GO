using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Feature.User.Requests.Command;
using QuicklyGo.Feature.User.Requests.Queries;
using QuicklyGo.Models;
using QuicklyGo.Utils;
using System.Security.Claims;
using static QuicklyGo.Utils.FileManager;

namespace QuicklyGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> SendFile(IFormFileCollection files, string conversationId)
        {
            // Check if the user is a member of the conversation
            var creatorId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var conversation = await _mediator
                .Send(new GetConversationByIdQuery(conversationId, true));
            if (conversation == null)
            {
                return BadRequest("Conversation not found");
            }
            var foundInGroup = false;
            foreach (var uc in conversation.UserConversations)
            {
                if (uc.UserId == creatorId)
                {
                    foundInGroup = true;
                    break;
                }
            }
            if (!foundInGroup)
            {
                return BadRequest("User is not in the conversation");
            }

            // Save the files
            var results = new List<FileSaveResult>();
            foreach (var formFile in files)
            {
                try
                {
                    var fileSaveResult = await SaveFile(formFile, conversationId);
                    results.Add(fileSaveResult);
                }
                catch (Exception ex)
                {
                    foreach (var result in results)
                    {
                        System.IO.File.Delete(result.FilePath);
                    }
                    return BadRequest(ex.Message);
                }                
            }

            // Write the message to the database
            try
            {
                var listMessage = new List<Message>();
                foreach (var result in results)
                {
                    listMessage.Add(new Message { CreatorId = creatorId, ConversationId = conversationId, BodyContent = result.FilePath });
                }
                await _mediator.Send(new AddNewMessagesCommand(listMessage));
            } 
            catch (Exception ex)
            {
                foreach (var result in results)
                {
                    System.IO.File.Delete(result.FilePath);
                }
                return BadRequest(ex.Message);
            }

            return Ok(results);
        }

        // POST: api/file/avatar-upload
        [HttpPost("avatar-upload")]
        public async Task<ActionResult<User>> UploadAvatar(IFormFile file)
        {
            // Save the files
            var result = new FileSaveResult { FileName = "", FilePath = "" };
            try
            {
                result = await SavePicture(file, "avatar");
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(result.FilePath);
                return BadRequest(ex.Message);
            }

            try
            {
                // Get the user
                var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _mediator.Send(new GetUserByIdQuery(userId));

                // Update user avatar url
                user.UrlImgAvatar = result.FilePath + "\\" + result.FileName;
                await _mediator.Send(new UpdateUserCommand(user));
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(result.FilePath);
                return Problem(ex.Message);
            }
            
            return Ok(result);
        }
        [HttpPost("background-upload")]
        public async Task<ActionResult<User>> UploadBackground(IFormFile file)
        {
            // Save the files
            var result = new FileSaveResult { FileName = "", FilePath = "" };
            try
            {
                result = await SavePicture(file, "background");
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(result.FilePath);
                return BadRequest(ex.Message);
            }

            try
            {
                // Get the user
                var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _mediator.Send(new GetUserByIdQuery(userId));

                // Update user avatar url
                user.UrlBackground = result.FilePath + "\\" + result.FileName;
                await _mediator.Send(new UpdateUserCommand(user));
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(result.FilePath);
                return Problem(ex.Message);
            }

            return Ok(result);
        }
    }
}
