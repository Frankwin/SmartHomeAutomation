using System.Collections.Generic;
using SmartHomeAutomation.Web.Models;

namespace SmartHomeAutomation.Web.Infrastructure
{
    public static class UserRepository
    {
        public static List<AppUser> Users { get; set; }

        static UserRepository()
        {
            Users = new List<AppUser>();
        }
    }
}
