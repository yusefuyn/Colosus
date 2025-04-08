using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class PaymentTypeFirmRelation : IDBObject
    {
        public PaymentTypeFirmRelation()
        {
            CreateDate = DateTime.Now;
        }
        [Key]
        public int Key { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string PaymentTypePrivateKey { get; set; }
        public string FirmPrivateKey { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
