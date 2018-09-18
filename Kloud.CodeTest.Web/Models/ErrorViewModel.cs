namespace Kloud.CodeTest.Web.Models
{
    /// <summary>
    /// ErrorViewModel Class
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}