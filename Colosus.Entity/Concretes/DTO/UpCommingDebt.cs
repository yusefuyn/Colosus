using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class UpCommingDebt
    {

        public DateTime PayDate { get; set; }
        public DebitType Type { get; set; }
        public string CustomerPublicKey { get; set; }
        public string CustomerName { get; set; }
        public string PublicKey { get; set; }
        public decimal Price { get; set; }
        public bool Payed { get; set; }
        public string? Note { get; set; }
    }
}
