using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnityAuthorization.Models
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
