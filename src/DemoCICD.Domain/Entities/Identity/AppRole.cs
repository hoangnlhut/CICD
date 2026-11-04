using Microsoft.AspNetCore.Identity;

namespace DemoCICD.Domain.Entities.Identity
{
    //this is role group (nhóm quyền) for user, for example: Admin, User, Manager, Director, etc.
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
        public string RoleCode { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
        public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
