using AccountApp.Service;
using AccountApp.UseCases;
using AccountApp.UseCases.AddAccount;
using AccountApp.UseCasesInterfaces;
using Moq;
using NUnit.Framework;
using Repository.Interfaces;
using Repository.UnitOfWork;
using SharedLibrary;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aplication.Tests.Account.UseCases
{
    [TestFixture]
    class AddAccountTests
    {
        private string INVALID_DESCRIPTION = "";
        private string VALID_DESCRIPTION = "Conta de salário";
        private string VALID_NAME = "Conta Itaú";
        private string INVALID_NAME = "";
        private decimal VALID_BALANCE = 10;


        private IAccountService GetUseCase(IUnitOfWork unitOfWork = null, IAccountRepository repository = null)
        {
            return new AccountService(new AddAccount(unitOfWork, repository));
        }

        private IAddAccountRequestObject GetInvalidNameRequestObject()
        {
            return new AddAccountRequestObject(INVALID_NAME, VALID_DESCRIPTION, VALID_BALANCE);
        }

        private IAddAccountRequestObject GetInvalidDescriptionRequestObject()
        {
            return new AddAccountRequestObject(VALID_NAME, INVALID_DESCRIPTION, VALID_BALANCE);
        }

        private IAddAccountRequestObject GetValidRequestObject()
        {
            return new AddAccountRequestObject(VALID_NAME, VALID_DESCRIPTION, VALID_BALANCE);
        }

        private Mock<IAccountRepository> GetAccountRepositoryMock()
        {
            return new Mock<IAccountRepository>();
        }

        private Mock<IUnitOfWork> GetUowMock()
        {
            return new Mock<IUnitOfWork>();
        }

        [Test]
        [TestCase(TestName = "Add account with invalid name", Category = "Account App", Description = "Add new account", TestOf = typeof(AddAccount))]
        public async Task WhenGivenInvalidName_ShouldReturnBadRequest()
        {
            // Arrange
            var useCase = GetUseCase();
            var requestObject = GetInvalidNameRequestObject();

            // Act
            var response = await useCase.Handle(requestObject);

            // Assert
            var test = response.ValidationNotifications.GetType();
            var message = new ValidationNotification("name", "Nome não pode ser em branco");
            
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.ValidationNotifications.Any(x=> x.Field == "name" & x.Message == "Nome não pode ser em branco" ));
            //CollectionAssert.Contains(response.ValidationNotifications, new ValidationNotification("name", "Nome não pode ser em branco"));
        }

        [Test]
        [TestCase(TestName = "Add account with invalid Description", Category = "Account App", Description = "Add new account", TestOf = typeof(AddAccount))]
        public async Task WhenGivenInvalidDescription_ShouldReturnBadRequest()
        {
            // Arrange
            var useCase = GetUseCase();
            var requestObject = GetInvalidDescriptionRequestObject();

            // Act
            var response = await useCase.Handle(requestObject);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.ValidationNotifications.Any(x => x.Field == "description" & x.Message == "Descrição é obrigatória"));
        }

        [Test]
        [TestCase(TestName = "Add AAccount with existing description", Category = "Account App", Description = "Add new product", TestOf = typeof(AddAccount))]
        public async Task WhenGivenAnExistingDescription_ShouldReturnBadRequest()
        {
            // Arrange
            var repositoryMock = GetAccountRepositoryMock();
            var uowMock = GetUowMock();
            
            repositoryMock
                .Setup(r => r.CheckIfAccountExistsByDescription(It.IsAny<String>()))
                .Returns(Task.FromResult(true));
            
            
            var useCase = GetUseCase(uowMock.Object, repositoryMock.Object);
            var requestObject = GetValidRequestObject();

            // Act
            var response = await useCase.Handle(requestObject);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.ValidationNotifications.Any(x => x.Field == "description" & x.Message == "Já existe uma conta com a mesma descrição"));
        }
    }
}
