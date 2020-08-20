using System;
using System.Collections.Generic;
using System.Text;

namespace AccountApp.UseCases
{
    public class AccountResponse
    {
        public AccountResponse(Guid id, string description, string name)
        {
            Id = id;
            Description = description;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Description { get; private set; }

        public string Name { get; private set; }
    }
}
