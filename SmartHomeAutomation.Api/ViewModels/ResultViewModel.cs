using SmartHomeAutomation.Web.Enums;

namespace SmartHomeAutomation.Web.ViewModels
{
    public class ResultViewModel
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
