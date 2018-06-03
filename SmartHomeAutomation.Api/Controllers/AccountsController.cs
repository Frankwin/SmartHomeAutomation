﻿using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Domain.Models.Account;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;
        
        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Get all the accounts
        /// </summary>
        /// <returns>List of accounts in JSON format</returns>
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(accountService.GetAll());
        }

        /// <summary>
        /// Get an individual account
        /// </summary>
        /// <param name="guid">Account GUID</param>
        /// <returns>Account object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetAccount([FromRoute] Guid guid)
        {
            var account = accountService.GetByAccountGuid(guid);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Update an account
        /// </summary>
        /// <param name="guid">Account GUID from URL</param>
        /// <param name="account">Account object from body</param>
        /// <returns>Updated account object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutAccount([FromRoute] Guid guid, [FromBody] Account account)
        {
            if (guid != account.AccountId)
            {
                return BadRequest();
            }

            return Ok(accountService.Upsert(account, User));
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="account">Account object from body</param>
        /// <returns>Newly created account object in JSON format</returns>
        [HttpPost]
        public IActionResult PostAccount([FromBody] Account account)
        {
            return Ok(accountService.Upsert(account, User));
        }

        /// <summary>
        /// Delete an account
        /// </summary>
        /// <param name="guid">Account GUID from URL</param>
        /// <returns>Deleted account object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteAccount([FromRoute] Guid guid)
        {
            var account = accountService.SoftDelete(guid, User);
            return Ok(account);
        }
    }
}