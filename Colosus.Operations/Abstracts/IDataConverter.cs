using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Abstracts
{
    public interface IDataConverter
    {
        public string Serialize(object obj);
        public T Deserialize<T>(string data);
        public T Deserialize<T>(byte[] data);
    }
}
