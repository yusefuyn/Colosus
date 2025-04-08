using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Business.Concretes
{
    public class DataBaseSetting
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }
        public EnumDbType Type { get; set; }
    }

    public enum EnumDbType
    {
        Mysql,
        Postgresql,
        SqlLite,
        MsSql,
    }
}
