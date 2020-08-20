using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Identity.Domain
{
    public class ApplicationRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
        {
            ApplicationRoleId = Guid.NewGuid();
            Name = name;
        }

        [Key]
        public Guid ApplicationRoleId { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}