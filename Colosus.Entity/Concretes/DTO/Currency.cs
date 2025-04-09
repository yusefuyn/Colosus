using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class Currency
    {
        public string PublicKey { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool? Added { get; set; }

    }
}
