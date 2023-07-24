using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class UserProfileUpdateModel : ProfileModelBase
    {
        public object ImageFile { get; set; }
    }
}
