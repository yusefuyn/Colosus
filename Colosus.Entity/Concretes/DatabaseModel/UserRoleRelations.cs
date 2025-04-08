using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class UserRoleRelations : IDBObject
    {
        public UserRoleRelations()
        {
            CreateDate = DateTime.Now;
        }
        [Key]
        public int Key { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string UserPrivateKey { get; set; }
        public string RolePrivateKey { get; set; }
        public string IssuerPrivateKey { get; set; }
        public DateTime CreateDate { get; }
    }
}
