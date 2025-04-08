using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class CorporateCustomer : Colosus.Entity.Concretes.DatabaseModel.CorporateCustomer
    {
        public CorporateCustomer()
        {
            Address = "";
            Name = "";
            TaxNo = "";
            TaxOffice = "";
            ContactAddresses = new();
            PaymentAddresses = new();
            FirmPublicKey = "";
        }
        public List<ContactAddress> ContactAddresses { get; set; }
        public List<PaymentAddress> PaymentAddresses { get; set; }
        public string FirmPublicKey { get; set; }
    }
}
