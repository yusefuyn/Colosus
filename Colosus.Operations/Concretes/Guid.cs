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

        private string _generate() => $"{System.Guid.NewGuid()}-{DateTime.Now.ToString()}";
        public string Generate(string StartInd) => $"{StartInd.ToString()}-{_generate()}";
        public string Generate(string StartInd, string LastInd) => UrlRewrite($"{Generate(StartInd.ToString())}-{LastInd.ToString()}");
        private string UrlRewrite(string data)
        {
            string returned = data;
            char[] redChars = new char[] { '"', '/', ':', ' ', '#', '?', '&', '=', '%', '<', '>', '{', '}', '[', ']', '\\' };
            foreach (var redChar in redChars)
                returned = returned.Replace(redChar.ToString(), string.Empty);
            return returned;
        }
    }
}
