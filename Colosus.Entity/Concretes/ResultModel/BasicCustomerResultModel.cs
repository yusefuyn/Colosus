using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.ResultModel
{
    public class BasicCustomerResultModel : ICustomer
    {
        public string CustomerKey { get; set; }
        public string PublicKey { get; set; }
        public string ContactGroupKey { get; set; }
        public string PaymentGroupKey { get; set; }
        public bool VisibleFastOperation { get; set; }
        public string Name { get; set; }

        public string GetName()
        => Name;
    }
}
