using AccountApp.UseCasesInterfaces;
using Domain;
using Repository.Interfaces;
using Repository.UnitOfWork;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.UseCases.AddAccount
{
    public class AddAccount : IAddAccount
    {
        private readonly IUnitOfWork _uow;
        private readonly IAccountRepository _accountRepository;

        public AddAccount(IUnitOfWork uow, IAccountRepository accountRepository)
        {
            _uow = uow;
            _accountRepository = accountRepository;
        }

        public async Task<IAddAccountResponseObject> Handle(IAddAccountRequestObject requestObject)
        {
            try
            {
                requestObject.Validate();

                if (!requestObject.IsValid)
                    return new AddAccountResponseObject((int)HttpStatusCode.BadRequest, requestObject.ValidationNotifications);

                if (await DescriptionExists(requestObject.Description))
                    return new AddAccountResponseObject((int)HttpStatusCode.BadRequest,
                        new ValidationNotification("description", "Já existe uma conta com a mesma descrição"));

                var account = new Account(Guid.NewGuid(), requestObject.Name, requestObject.Description, requestObject.Balance);

                if (!await _uow.Commit())
                    return new AddAccountResponseObject((int)HttpStatusCode.InternalServerError,
                        new ValidationNotification("", "Erro ao gravar no banco de dados. Contate o Suporte."));

                return new AddAccountResponseObject(account);
            }
            catch (Exception e)
            {
                return new AddAccountResponseObject((int)HttpStatusCode.InternalServerError, new ValidationNotification("", "Ocorreu um erro no servidor: " + e.Message));
            }
        }

        private async Task<bool> DescriptionExists(string description)
        {
            return await _accountRepository.CheckIfAccountExistsByDescription(description);
        }
    }
}
