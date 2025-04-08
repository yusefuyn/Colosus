using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class Stock
    {
        public string ProductPublicKey { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; }
    }
}
