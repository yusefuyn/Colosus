using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.RequestModel
{
    public class SaleRequestModel
    {
        public SaleRequestModel()
        {
            CreateDate = new DateTime();
            SaleRequestModelToken = Guid.NewGuid().ToString();
        }

        public string CustomerPublicKey { get; set; }
        public string ProductPublicKey { get; set; }
        public string UserPublicKey { get; set; }
        public string UserName { get; set; }
        public string FirmPublicKey { get; set; }
        public string SaleRequestModelToken { get; set; } // Bu Soket iletişiminde işlem için kullanılacak olan token.
        public int SalesAmount { get; set; } = 1;
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Discount { get; set; } = 0;

    }
}
