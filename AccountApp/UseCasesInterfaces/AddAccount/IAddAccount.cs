using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.UseCasesInterfaces
{
    public interface IAddAccount
    {
        //recebe um request object
        //retorna um response object

        Task<IAddAccountResponseObject> Handle(IAddAccountRequestObject requestObject);
    }
}
