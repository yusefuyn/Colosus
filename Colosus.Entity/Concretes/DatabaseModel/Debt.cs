using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class Debt : IDBObject
    {
        public Debt()
        {
            CreateDate = DateTime.Now;
            PayDate = DateTime.Now;
        }
        [Key]
        public int Key { get;set; }
        public string PrivateKey { get;set; }
        public string UserPrivateKey { get; set; }
        public string PublicKey { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerKey { get; set; }
        public string? Note { get; set; }
        public decimal Price { get; set; }
        public DateTime PayDate { get; set; }
        public DebitType Type { get; set; }

    }
    public enum DebitType { 
        Take, // Alacak
        Give
    }
}
