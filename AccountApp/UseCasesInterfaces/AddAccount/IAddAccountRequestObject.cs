using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountApp.UseCasesInterfaces
{
    public interface IAddAccountRequestObject : IValidator
    {
        decimal Balance { get; }

        string Name { get;}

        string Description { get; }
    }
}
