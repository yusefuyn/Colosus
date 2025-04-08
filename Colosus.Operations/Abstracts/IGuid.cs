using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Abstracts
{
    public interface IGuid
    {
        /// <summary>
        /// Başlangıç ve bitiş anahtarları üretilen anahtarın hangi sınıfa ve hangi işleme ait olduğunu gösterir.
        /// Sadece Anahtara bakarak bile obje hakkında bir çok bilgi sağlar.
        /// </summary>
        /// <param name="StartInd"></param>
        /// <param name="LastInd"></param>
        /// <returns></returns>
        public string Generate(string StartInd);
        /// <summary>
        /// Başlangıç ve bitiş anahtarları üretilen anahtarın hangi sınıfa ve hangi işleme ait olduğunu gösterir.
        /// Sadece Anahtara bakarak bile obje hakkında bir çok bilgi sağlar.
        /// </summary>
        /// <param name="StartInd"></param>
        /// <param name="LastInd"></param>
        /// <returns></returns>
        public string Generate(string StartInd, string LastInd);
    }
}
