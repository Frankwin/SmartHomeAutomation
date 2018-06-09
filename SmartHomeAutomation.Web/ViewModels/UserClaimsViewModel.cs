using System.Collections.Generic;

namespace SmartHomeAutomation.Web.ViewModels
{
    public class UserClaimsViewModel
    {
        public IEnumerable<ClaimViewModel> Claims { get; set; }
        public string UserName { get; set; }
    }
}
