namespace QuicklyGo.Models
{
    public enum Roles
    {
        Admin = 0,
        User = 1
    }
    public enum UserStatus
    {
        Deleted = 0,
        Active = 1,
        Pending = 2
    }
    public enum UserConversationStatus
    {
        Deleted = 0,
        Active = 1
    }
    public enum ConversationStatus
    {
        Deleted = 0,
        Active = 1
    }
    public enum MessageStatus
    {
        Deleted = 0,
        Active = 1,
        Pending = 2
    }
    public enum RelationshipStatus
    {
        Deleted = 0,
        Active = 1,
        WaitingForAccept = 2,
        Pending = 3,
        Declined = 4,
        Blocked = 5
    }
    public enum NoticeStatus
    {
        Deleted = 0,
        NotRead = 1,
        Read = 2
    }
}
