using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Abstracts
{
    public interface IMapping
    {
        public T Convert<T>(object obj);
        public List<T> ConvertToList<T>(object objs);
    }
}
