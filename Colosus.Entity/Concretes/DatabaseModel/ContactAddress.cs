using Colosus.Entity.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Entity.Concretes.DatabaseModel
{
    public class ContactAddress : IDBObject
    {
        public ContactAddress()
        {
            CreateDate = DateTime.Now;
            Type = "";
            Value = "";
            Note = "";
        }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }
        [Key]
        public int Key { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        /// <summary>
        /// Bu anahtar iletişim kurulabilecek mekan,bireysel veya kurumsal müşteri objesindeki anahtarı temsil etmektedir.
        /// </summary>
        public string ContactGroupKey { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
