using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class ValidateEmailUserRequest : IRequest<bool>
    {
        public string UserId { get; set; }
        public string CodeValidateEmail { get; set; }
        public ValidateEmailUserRequest(string id, string CodeValidateEmail)
        {
            this.UserId = id;
            this.CodeValidateEmail = CodeValidateEmail;
        }
    }
}
