using Colosus.Entity.Concretes.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    public class IndividualCustomerCreateModel : Colosus.Entity.Concretes.DatabaseModel.IndividualCustomer
    {
        public IndividualCustomerCreateModel()
        {
            ContactAddresses = new();
        }
        public List<ContactAddress> ContactAddresses { get; set; }
        public string FirmPublicKey { get; set; }
    }
}
