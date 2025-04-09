using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class CorporateCustomerCreateModel : ICustomer, ICorporate
    {
        public CorporateCustomerCreateModel()
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
        public string Name { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public string Address { get; set; }
        public string CustomerKey { get;set; }
        public string ContactGroupKey { get;set; }
        public string PaymentGroupKey { get;set; }
        public bool VisibleFastOperation { get; set; }

        public string GetName()
        => $"{Name}";
    }
}
