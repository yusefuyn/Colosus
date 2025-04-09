using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.RequestModel
{
    public class LoginUserRequestModel
    {
        public LoginUserRequestModel()
        {
            HashedToPass = true;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool HashedToPass { get; set; }
    }
}
