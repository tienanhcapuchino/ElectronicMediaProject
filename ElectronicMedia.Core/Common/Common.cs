using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core
{
    public static class Common
    {
        public static string Decode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
