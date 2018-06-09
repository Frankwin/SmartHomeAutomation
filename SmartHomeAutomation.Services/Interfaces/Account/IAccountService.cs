using System;
using System.Security.Principal;

namespace SmartHomeAutomation.Services.Interfaces.Account
{
    public interface IAccountService : IReaderService<Domain.Models.AccountModels.Account>, IWriterService<Domain.Models.AccountModels.Account>
    {
        Domain.Models.AccountModels.Account Upsert(Domain.Models.AccountModels.Account account, IPrincipal userPrincipal);
        Domain.Models.AccountModels.Account SoftDelete(Guid guid, IPrincipal userPrincipal);
        Domain.Models.AccountModels.Account CheckForExistingAccountName(string name);
    }
}
