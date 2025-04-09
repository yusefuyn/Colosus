using Colosus.Entity.Abstracts;
using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class IndividualCustomerCreateModel : ICustomer, IIndividual
    {
        public IndividualCustomerCreateModel()
        {
            ContactAddresses = new();
        }
        public List<ContactAddress> ContactAddresses { get; set; }
        public string FirmPublicKey { get; set; }
        public string CustomerKey { get; set; }
        public string ContactGroupKey { get; set; }
        public string PaymentGroupKey { get; set; }
        public bool VisibleFastOperation { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? IDCardNumber { get; set; }
        public IndividualCustomerTypeEnum CustomerTypeEnum { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Note { get; set; }

        public string GetName()
        => $"{Name} {MiddleName} {LastName}";
    }
}
