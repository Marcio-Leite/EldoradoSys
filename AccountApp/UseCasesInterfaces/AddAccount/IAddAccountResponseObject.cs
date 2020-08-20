using AccountApp.UseCases;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountApp.UseCasesInterfaces
{
    public interface IAddAccountResponseObject : IResponse
    {
        AccountResponse AccountResponse { get; }
    }
}
