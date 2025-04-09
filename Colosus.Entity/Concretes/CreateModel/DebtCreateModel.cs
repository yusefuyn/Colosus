using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class DebtCreateModel
    {
        public DebtCreateModel()
        {
            Note = "";
            Price = 0;
        }
        public string Note { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Price { get; set; }
        public string CustomerPublicKey { get; set; }
        public DebitType Type { get; set; }
    }
}
