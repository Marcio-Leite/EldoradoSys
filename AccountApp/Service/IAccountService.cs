using AccountApp.UseCasesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Service
{
    public interface IAccountService
    {
        Task<IAddAccountResponseObject> Handle(IAddAccountRequestObject requestObject);
    }
}
