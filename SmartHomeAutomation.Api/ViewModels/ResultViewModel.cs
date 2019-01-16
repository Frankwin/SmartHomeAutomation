using SmartHomeAutomation.Api.Enums;

namespace SmartHomeAutomation.Api.ViewModels
{
    public class ResultViewModel
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
