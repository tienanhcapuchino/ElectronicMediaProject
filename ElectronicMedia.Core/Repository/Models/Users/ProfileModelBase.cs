using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class ProfileModelBase
    {
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
