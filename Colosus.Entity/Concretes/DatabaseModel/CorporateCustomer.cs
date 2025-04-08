using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class CorporateCustomer : IDBObject, ICustomer, ICorporate
    {
        public CorporateCustomer()
        {
            CreateDate = DateTime.Now;
        }
        public string CustomerKey { get;set; }
        public string ContactGroupKey { get;set; }
        public string PaymentGroupKey { get; set; }
        [Key]
        public int Key { get;set; }
        public string PrivateKey { get;set; }
        public string PublicKey { get;set; }
        public string Name { get;set; }
        public string TaxOffice { get;set; }
        public string TaxNo { get;set; }
        public string Address { get;set; }
        public DateTime CreateDate { get; set; }

        public string GetName() => $"{Name}";
    }
}
