using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class Firm : IDBObject
    {
        [Key]
        public int Key { get; set;  }
        public string PrivateKey { get; set;  }
        /// <summary>
        /// İç paylaşım anahtarı
        /// </summary>
        public string PublicKey { get; set;  }
        public string Name { get; set; }
        /// <summary>
        /// Dış servislerde temsili. Entegrasyonlar vs.
        /// </summary>
        public string PublicID { get; set; }
    }
}
