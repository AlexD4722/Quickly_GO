namespace QuicklyGo.Reponses
{
    public class TokenGenerateResponse
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
