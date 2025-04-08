using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.CreateModel
{
    /// <summary>
    /// Yeni ürün eklerken kullanılacak obje
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string CategoryPublicKey { get; set; }
        public string FirmPublicKey { get; set; }
    }
}
