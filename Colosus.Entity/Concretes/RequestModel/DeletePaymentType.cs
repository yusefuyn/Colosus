using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.RequestModel
{
    public class DeletePaymentType
    {
        public string FirmPublicKey { get; set; }
        public string PaymentTypePublicKey { get; set; }
    }
}
