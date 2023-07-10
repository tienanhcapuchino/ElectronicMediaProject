using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{

    public class UserIdentity : IdentityUser
    {
        public string FullName { get;set; }
        public DateTime Dob { get; set; }
        public string? Image { get; set; }
        public bool IsActived { get; set; }
        public Gender? Gender { get; set; }
        public virtual Department? Department { get; set; }
        public Guid? DepartmentId { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<EmailTemplate>? EmailTemplates { get; set; }
    }
    public enum Gender : byte
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }
}
