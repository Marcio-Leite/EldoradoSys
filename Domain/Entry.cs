using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Entry
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public DateTime EntryDateTime { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public EntryType Type { get; set; }
    }

    public enum EntryType
    {
        Debit = 0,
        Credit = 1
    }
}
