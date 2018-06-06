using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Account;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IAccountService : IReaderService<Account>, IWriterService<Account>
    {
        Account Upsert(Account account, IPrincipal userPrincipal);
        Account SoftDelete(Guid guid, IPrincipal userPrincipal);
        Account CheckForExistingAccountName(string name);
    }
}
