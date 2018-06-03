using System;
using System.Collections.Generic;
using SmartHomeAutomation.Domain.Models.Account;
using SmartHomeAutomation.Domain.Models.User;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IAccountService : IReadService<Account>, IWriteService<Account>
    {
        Guid GenerateGuid();
        PagingResult GetAccountsByPage(int pageSize, int pageNumber, string orderBy, string direction);
        Account GetByAccountGuid(Guid guid);
        Account Upsert(Account account);
        List<User> GetUsers(Guid guid);
    }
}
