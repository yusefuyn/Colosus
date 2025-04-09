using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class DebtDTO
    {
        public DebtDTO()
        {
            CustomerName = "";
            CustomerPublicKey = "";
            Pays = new();
        }
        public string PublicKey { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerKey { get; set; }
        public string CustomerPublicKey { get; set; }
        public string CustomerName { get; set; }
        public string? Note { get; set; }
        public decimal Price { get; set; }
        public DateTime PayDate { get; set; }
        public bool Payed() => Pays.Select(xd => xd.Price).Sum() == this.Price ? true : false;
        public DebitType Type { get; set; }
        public List<DebtPayDTO> Pays { get; set; }

    }
}
