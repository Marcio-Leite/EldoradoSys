using System;
using System.Collections.Generic;

namespace Domain.Identity
{
    public class Cronograma
    {
        public Guid Id { get; set; }
        
        public List<ItemCronograma> ItensCronograma { get; set; }
    }
}