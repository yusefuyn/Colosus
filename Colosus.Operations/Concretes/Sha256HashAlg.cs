using Colosus.Operations.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Concretes
{
    public class Sha256HashAlg : IHash
    {
        public string Calc(string Data)
        {
            using (SHA256 sha256Hash = SHA256.Create())  
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));  
                return builder.ToString();  
            }
        }
    }
}
