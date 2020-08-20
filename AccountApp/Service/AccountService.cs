using AccountApp.UseCasesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAddAccount _addAccount;

        public AccountService(IAddAccount addAccount = null)
        {
            _addAccount = addAccount;
        }

        public async Task<IAddAccountResponseObject> Handle(IAddAccountRequestObject requestObject)
        {
            return await _addAccount.Handle(requestObject);
        }
    }
}
