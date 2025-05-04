using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class RegisterCreateModel
    {
        public RegisterCreateModel()
        {
            CreateDate = DateTime.Now;
        }
        public bool PasswdHashed { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; private set; }

    }
}
