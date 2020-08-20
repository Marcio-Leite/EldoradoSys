using System;

namespace Domain.Identity
{
    public class RoleToUser
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}