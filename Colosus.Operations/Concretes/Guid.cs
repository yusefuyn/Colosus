using Colosus.Operations.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Concretes
{
    public class Guid : IGuid
    {

        private string _generate() => $"{System.Guid.NewGuid()}-{DateTime.Now.ToString().Replace(" ","")}";
        public string Generate(string StartInd) => $"{StartInd.ToString()}-{_generate()}";
        public string Generate(string StartInd, string LastInd) => $"{Generate(StartInd.ToString())}-{LastInd.ToString()}";
    }
}
