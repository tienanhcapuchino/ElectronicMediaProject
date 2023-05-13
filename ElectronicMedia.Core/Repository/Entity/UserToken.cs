using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string? VerifyToken { get; set; }
        public DateTime? VerifyExpiryDate { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPassExpiryDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }

    }
}
