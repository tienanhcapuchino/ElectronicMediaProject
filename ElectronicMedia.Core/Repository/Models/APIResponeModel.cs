using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class APIResponeModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
        public object? Data { get; set; }
    }
}
