using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class Category : IDBObject
    {
        public Category()
        {
            CreateDate = DateTime.Now;
            Tax = 0;
        }
        [Key]
        public int Key { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
