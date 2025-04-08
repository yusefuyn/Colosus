using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class ProductCategoryRelation : IDBObject
    {
        public ProductCategoryRelation()
        {
            CreateDate = DateTime.Now;
        }
        [Key]
        public int Key { get; set;  }
        public string PrivateKey { get; set;  }
        public string PublicKey { get; set;  }
        public string ProductPrivateKey { get; set; }
        public string CategoryPrivateKey { get; set; }
        public DateTime CreateDate { get; }
    }
}
