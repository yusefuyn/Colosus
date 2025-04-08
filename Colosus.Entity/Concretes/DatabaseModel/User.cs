using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class User : IDBObject
    {
        public User()
        {
            CreateDate = DateTime.Now;
            UserName = "";
            Password = "";
            EMail = "";
            FirstName = "";
            LastName = "";
        }

        [Key]
        public int Key { get; set;  }
        public string PrivateKey { get; set;  }
        public string PublicKey { get; set;  }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string? ReferancePrivateKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; private set; }
    }
}
