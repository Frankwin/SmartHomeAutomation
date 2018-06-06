using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Account;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services
{
    public class AccountService : BaseService<Account, SmartHomeAutomationContext>, IAccountService
    {
        public AccountService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public Account Upsert(Account account, IPrincipal userPrincipal)
        {
            var existingAccount = CheckForExistingAccountName(account.AccountName);
            if (existingAccount != null)
            {
                if (existingAccount.IsDeleted)
                {
                    existingAccount.IsDeleted = false;
                    account = existingAccount;
                }
                else
                {
                    throw new DuplicateNameException($"An account with name '{account.AccountName}' already exists");
                }
            }

            if (account.AccountId == Guid.Empty)
            {
                account.SetInsertInfo(userPrincipal);
                Insert(account);
            }
            else
            {
                account.SetUpdateInfo(userPrincipal);
                Update(account);
            }

            return account;
        }

        public Account SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var account = context.Accounts.FirstOrDefault(a => a.AccountId == guid);
                if (account == null)
                {
                    throw new ArgumentException($"Account with ID {guid} does not exist");
                }
                account.SetDeleteInfo(userPrincipal);
                Update(account);
                return account;
            }
        }

        public Account CheckForExistingAccountName(string name)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var existingAccount = context.Accounts.FirstOrDefault(x => x.AccountName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                return existingAccount;
            }
        }
    }
}
