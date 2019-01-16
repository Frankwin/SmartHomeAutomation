using System.Collections.Generic;

namespace SmartHomeAutomation.Api.ViewModels
{
    public class UserClaimsViewModel
    {
        public IEnumerable<ClaimViewModel> Claims { get; set; }
        public string UserName { get; set; }
    }
}
