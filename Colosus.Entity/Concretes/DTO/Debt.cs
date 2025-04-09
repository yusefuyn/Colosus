using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class Debt
    {
        public Debt()
        {
            Debts = new();
            CustomerPublicKey = "";
            CustomerName = "";
        }
        public string CustomerPublicKey { get; set; }
        public string CustomerName { get; set; }
        public List<Colosus.Entity.Concretes.DatabaseModel.Debt> Debts { get; set; }


    }
}
