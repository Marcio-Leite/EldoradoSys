using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Identity.Domain
{
    public class ApplicationUserRole
    {

        public ApplicationUserRole()
        {
            
        }   
        public ApplicationUserRole(ApplicationUser userId, ApplicationRole roleId)
        {
            //ApplicationUser = userId;
            //ApplicationRole = roleId;
            ApplicationUserId = userId.ApplicationUserId;
            ApplicationRoleId = roleId.ApplicationRoleId;
        }
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [Key]
        public Guid ApplicationRoleId { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }
       
       
    }
}