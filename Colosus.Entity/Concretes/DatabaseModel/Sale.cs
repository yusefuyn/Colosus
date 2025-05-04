using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class Sale : IDBObject
    {
        public Sale()
        {
            SalesAmount = 1;
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }
        [Key]
        public int Key { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string CustomerPrivateKey { get; set; }
        public string UserPrivateKey { get; set; }
        public string ProductPrivateKey { get; set; }
        public string FirmPrivateKey { get; set; }
        public int SalesAmount { get; set; } = 1;
        public string? PaymentTypePrivateKey { get; set; }
        public string? CurrencyTypePrivateKey { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// Temel satış grup anahtarı.
        /// </summary>
        public string SalesGroupKey { get; set; }
        /// <summary>
        /// Ödeme parça parça yapılacaksa bu grup ile gruplanır.
        /// </summary>
        public string PaymentGroupKey { get; set; }

    }
}
