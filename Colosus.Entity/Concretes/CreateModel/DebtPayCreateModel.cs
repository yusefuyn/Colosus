using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class DebtPayCreateModel
    {

        public string DebtPublicKey { get; set; }
        public string PaymentTypePublicKey { get; set; }
        public string CurrencyPublicKey { get; set; }
        public decimal Price { get; set; }
    }
}
