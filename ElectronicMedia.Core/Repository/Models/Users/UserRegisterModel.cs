using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class UserRegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Repassword { get; set; }
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public string? Avatar { get; set; }
        public string PhoneNumber { get; set; }
    }
}
