using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Account
    {
        public Account()
        {

        }

        public Account(Guid id, string name, string description, decimal balance)
        {
            Id = id;
            Description = description;
            Name = name;
            Balance = balance;
        }
        
        [Key]
        public Guid Id { get; set; }

        public decimal Balance { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
