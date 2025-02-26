using Shared.Enums;

namespace DataLayer.Entities
{
    public class Role : BaseEntity
    {
        public RoleEnum RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
