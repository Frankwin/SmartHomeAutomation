using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.User;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IUserService : IReadService<User>, IWriteService<User>
    {
        User Upsert(User user, IPrincipal userPrincipal);
        User SoftDelete(Guid guid, IPrincipal userPrincipal);
        User CheckForExistingUserName(string name);
    }
}
