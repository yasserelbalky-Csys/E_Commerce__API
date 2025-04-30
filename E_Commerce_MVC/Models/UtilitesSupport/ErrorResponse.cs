namespace E_Commerce_MVC.Models.UtilitesSupport
{
    public class ErrorResponse
    {
        public string? Title { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new ();
    }
}