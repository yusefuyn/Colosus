using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Abstracts
{
    public interface ICustomer
    {
        public string CustomerKey { get; set; }
        public string GetName();
        public string ContactGroupKey { get; set; }
        public string PaymentGroupKey { get; set; }
        public bool VisibleFastOperation { get; set; }
        public string Name { get; set; }

    }
}
