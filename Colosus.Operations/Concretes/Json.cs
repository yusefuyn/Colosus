using Colosus.Operations.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Concretes
{
    public class Json : IDataConverter
    {
        public T Deserialize<T>(string data) => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);

        public T Deserialize<T>(byte[] data) => System.Text.Json.JsonSerializer.Deserialize<T>(data);

        public string Serialize(object obj) => Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }
}
