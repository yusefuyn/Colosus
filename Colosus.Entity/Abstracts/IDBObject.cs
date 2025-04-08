using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Abstracts
{
    public interface IDBObject
    {
        [Key]
        /// DB için gerekli
        public int Key { get; set; }
        /// <summary>
        /// Kod kısmında kullanılan sabit
        /// </summary>
        public string PrivateKey { get; set; }
        /// <summary>
        /// Dışarıdan eklemelerde kullanılan sürekli değişken
        /// </summary>
        public string PublicKey { get; set; }
    }
}
