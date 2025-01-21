namespace DemoAzureMVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Added to show Error Message
        public string ErrorMessage { get; set; }
    }
}
