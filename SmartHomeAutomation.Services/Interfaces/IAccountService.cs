using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Account;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IAccountService : IReadService<Account>, IWriteService<Account>
    {
        Account GetByAccountGuid(Guid guid);
        Account Upsert(Account account, IPrincipal userPrincipal);
        Account SoftDelete(Guid guid, IPrincipal userPrincipal);
        Account CheckForExistingAccountName(string name);
    }
}
