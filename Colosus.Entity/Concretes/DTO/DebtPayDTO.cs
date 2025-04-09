using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class DebtPayDTO
    {

        public string PublicKey { get; set; }
        public DateTime CreateDate { get; set; }
        public string PaymentTypeName { get; set; }
        public string CurrencyName { get; set; }
        public decimal Price { get; set; }
    }
}
