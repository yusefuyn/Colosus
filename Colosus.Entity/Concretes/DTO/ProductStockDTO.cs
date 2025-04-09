using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DTO
{
    public class ProductStockDTO
    {
        public string PublicKey { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserPublicKey { get; set; }
        public string UserFirstAndLastName { get; set; }
        public string? Description { get; set; }

    }
}
