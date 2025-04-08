using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class IndividualCustomer : IDBObject, ICustomer, IIndividual
    {
        public IndividualCustomer()
        {
            CreateDate = DateTime.Now;
            CustomerTypeEnum = IndividualCustomerTypeEnum.Unknow;
            PrivateKey = "";
            PublicKey = "";
            ContactGroupKey = "";
            CustomerKey = "";
        }
        [Key]
        public int Key { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string? IDCardNumber { get; set; }
        public string? Note { get; set; }
        public string GetName() => $"{FirstName} {MiddleName} {LastName}";
        public IndividualCustomerTypeEnum CustomerTypeEnum { get; set; }
        /// <summary>
        /// Ödül kartı vs için kullanılacak anahtar.
        /// </summary>
        public string CustomerKey { get; set; }
        /// <summary>
        /// İletişim adresinin gruplanması için anahtar
        /// </summary>
        public string ContactGroupKey { get; set; }
        public string? PaymentGroupKey { get; set; }

    }
}
