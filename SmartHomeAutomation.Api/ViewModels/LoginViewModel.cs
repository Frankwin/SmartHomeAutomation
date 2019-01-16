using System.ComponentModel.DataAnnotations;

namespace SmartHomeAutomation.Api.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
