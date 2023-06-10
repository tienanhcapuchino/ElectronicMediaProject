using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public Gender? Gender { get; set; }
        public string Image
        {
            get
            {
                if (string.IsNullOrEmpty(this.Image))
                {
                    return Convert.ToBase64String(CommonService.InitAvatarUser());
                }
                else
                {
                    return this.Image;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(this.Image))
                {
                    this.Image = value;
                }
                else
                {
                    this.Image = Convert.ToBase64String(CommonService.InitAvatarUser());
                }
            }
        }
    }
}
