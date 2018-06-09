using System;
using System.Security.Principal;

namespace SmartHomeAutomation.Services.Interfaces.User
{
    public interface IUserService : IReaderService<Domain.Models.UserModels.User>, IWriterService<Domain.Models.UserModels.User>
    {
        Domain.Models.UserModels.User Upsert(Domain.Models.UserModels.User user, IPrincipal userPrincipal);
        Domain.Models.UserModels.User SoftDelete(Guid guid, IPrincipal userPrincipal);
        Domain.Models.UserModels.User CheckForExistingUserName(string name);
    }
}
