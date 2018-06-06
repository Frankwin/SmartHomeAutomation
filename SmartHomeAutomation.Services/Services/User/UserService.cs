using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.User;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services
{
    public class UserService : BaseService<User, SmartHomeAutomationContext>, IUserService
    {
        public UserService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public User Upsert(User user, IPrincipal userPrincipal)
        {
            var existingUser = CheckForExistingUserName(user.UserName);
            if (existingUser != null)
            {
                if (existingUser.IsDeleted)
                {
                    existingUser.IsDeleted = false;
                    user = existingUser;
                }
                else
                {
                    throw new DuplicateNameException($"An user with name '{user.UserName}' already exists");
                }
            }

            if (user.UserId == Guid.Empty)
            {
                user.SetInsertInfo(userPrincipal);
                Insert(user);
            }
            else
            {
                user.SetUpdateInfo(userPrincipal);
                Update(user);
            }

            return user;
        }

        public User SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var user = context.Users.FirstOrDefault(a => a.UserId == guid);
                if (user == null)
                {
                    throw new ArgumentException($"User with ID {guid} does not exist");
                }
                user.SetDeleteInfo(userPrincipal);
                Update(user);
                return user;
            }
        }

        public User CheckForExistingUserName(string name)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var existingUser = context.Users.FirstOrDefault(x => x.UserName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                return existingUser;
            }
        }
    }
}
