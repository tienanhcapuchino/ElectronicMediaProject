
﻿using System;
using System.Collections.Generic;
using System.IO;

﻿using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Common
{
    public static class UserRole
    {

        public const string Admin = "Admin";
        public const string NormalUser = "NormalUser";
        public const string Writer = "Writer";
        public const string Leader = "Leader";
        public const string EditorDirector = "EditorDirector";
        public static readonly List<string> Roles = new List<string>() { Admin, NormalUser, Writer, Leader, EditorDirector };

    }
}
