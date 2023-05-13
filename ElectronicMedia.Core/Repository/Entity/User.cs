using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public RoleType Role { get; set; }
        public string? Avatar { get; set; }
        public bool IsActived { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }

    public enum RoleType : byte
    {
        UserNormal = 0,
        Admin = 1,
        Writer = 2,
        Leader = 3,
        Editor = 4
    }
}
