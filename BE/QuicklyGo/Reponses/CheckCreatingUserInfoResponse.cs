namespace QuicklyGo.Reponses
{
    public class CheckCreatingUserInfoResponse
    {
        public bool UsernameExists { get; set; }
        public bool EmailExists { get; set; }
        public bool PhoneNumberExists { get; set; }
    }
}
