using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class AddNewNoticeHandler : IRequestHandler<AddNewNoticeCommand, Notice>
    {
        public IGenericRepository<Notice> _noticeRepository { get; set; }
        public AddNewNoticeHandler(IGenericRepository<Notice> noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }
        public async Task<Notice> Handle(AddNewNoticeCommand request, CancellationToken cancellationToken)
        {          
            var notice = await _noticeRepository.Add(request.Notice);
            await _noticeRepository.Save();
            return notice;
        }
    }
}
