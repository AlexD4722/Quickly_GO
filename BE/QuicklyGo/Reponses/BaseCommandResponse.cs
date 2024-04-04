namespace QuicklyGo.Reponses
{
    public class BaseCommandResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
