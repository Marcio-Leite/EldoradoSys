using AccountApp.UseCasesInterfaces;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountApp.UseCases
{
    public class AddAccountRequestObject : IAddAccountRequestObject
    {
        public AddAccountRequestObject(string name, string description, decimal balance)
        {
            Name = name;
            Description = description;
            Balance = balance;
            _validationNotifications = new List<ValidationNotification>();
        }

        public decimal Balance { get; private set; }
        public string Name { get; private set; }

        public string Description { get; private set; }


        private List<ValidationNotification> _validationNotifications { get; set; }
        public IEnumerable<ValidationNotification> ValidationNotifications => this._validationNotifications;

        public bool IsValid { get; private set; }


        public void Validate()
        {
            if (IsValidName() &&
                IsValidDescription())
                IsValid = true;
        }

        private bool IsValidDescription()
        {
            if (String.IsNullOrEmpty(Description))
            {
                _validationNotifications.Add(new ValidationNotification("description", "Descrição é obrigatória"));
                return false;
            }

            return true;
        }

        private bool IsValidName()
        {
            if (String.IsNullOrEmpty(Name))
            {
                _validationNotifications.Add(new ValidationNotification("name","Nome não pode ser em branco"));
                return false;
            }

            return true;
        }
    }
}
