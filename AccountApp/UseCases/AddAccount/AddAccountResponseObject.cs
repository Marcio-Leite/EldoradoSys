using AccountApp.UseCasesInterfaces;
using Domain;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AccountApp.UseCases
{
    public class AddAccountResponseObject : IAddAccountResponseObject
    {
        public AddAccountResponseObject(int statusCode, IEnumerable<ValidationNotification> validationNotifications)
        {
            StatusCode = statusCode;
            ValidationNotifications = validationNotifications;
        }

        public AddAccountResponseObject(int statusCode, ValidationNotification validationNotification)
        {
            StatusCode = statusCode;
            ValidationNotifications = new List<ValidationNotification> { validationNotification };
        }

        public AddAccountResponseObject(Account account)
        {
            StatusCode = (int)HttpStatusCode.OK;
            AccountResponse = new AccountResponse(account.Id, account.Description, account.Name);
        }

        public AccountResponse AccountResponse { get; private set; }

        public IEnumerable<ValidationNotification> ValidationNotifications { get; private set; }

        public int StatusCode { get; private set; }

    }
}
