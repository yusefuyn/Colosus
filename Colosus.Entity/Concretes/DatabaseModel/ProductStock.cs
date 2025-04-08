using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class ProductStock : IDBObject
    {
        public ProductStock()
        {
            CreateDate = DateTime.Now;
            Amount = 0;
        }
        [Key]
        public int Key {get; set; }
        public string PrivateKey {get; set; }
        public string PublicKey {get; set; }

        public DateTime CreateDate { get; set; }
        public string? UserPrivateKey { get; set; }
        public string ProductPrivateKey { get; set; }
        public string? FirmPrivateKey { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
    }
}
