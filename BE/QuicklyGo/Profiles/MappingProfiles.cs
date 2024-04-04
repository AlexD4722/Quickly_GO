using AutoMapper;
using QuicklyGo.Data.DTOs.ChatHub;
using QuicklyGo.Data.DTOs.Conversation;
using QuicklyGo.Data.DTOs.Message;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Models;
using QuicklyGo.Unit;

namespace QuicklyGo.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>();
            CreateMap<User, GetInfoUserDto>().ReverseMap();
            CreateMap<User, GetInfoUserDto>();
            CreateMap<User, GetToViewInfoUserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.Id = CreateGenerateUniqueKey.GenerateUniqueKey();
                    dest.Password = BCrypt.Net.BCrypt.HashPassword(dest.Password);
                    dest.Status = UserStatus.Pending;
                    dest.CreateAt = DateTime.Now;
                    dest.UpdateAt = DateTime.Now;
                });
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>();
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Message, MessageDto>();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>();
            CreateMap<Conversation, ConversationInfoDTO>().ReverseMap();
            CreateMap<Conversation, ConversationInfoDTO>();
            /*  CreateMap<User, GetInfoUserDto>();
              CreateMap<User, UserDto>();
              CreateMap<User, UserDto>().ReverseMap();
              CreateMap<User, CreateUserDto>().ReverseMap()
                  .AfterMap((src, dest) =>
                  {
                      dest.KeyUniqueNow = CreateGenerateUniqueKey.GenerateUniqueKey();
                      dest.VerifyCode = "";
                      dest.Status = "Active";
                  });
              CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
              CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
              CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
              CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
              CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
              CreateMap<LeaveRequest, LeaveRequestListDto>().ReverseMap();*/
        }
    }
}
