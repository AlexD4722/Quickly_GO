using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class AddNewNoticeCommand : IRequest<Notice>
    {
        public Notice Notice { get; set; }
        public AddNewNoticeCommand(Notice notice)
        {
            Notice = notice;
        }
    }
}
