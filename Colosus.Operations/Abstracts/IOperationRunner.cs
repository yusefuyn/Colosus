using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Abstracts
{
    public interface IOperationRunner
    {

        public delegate void LogEvent(string message);
        public event LogEvent logEvent;
        public void ActionRunner(Action firstAct, Action errorAct);
    }
}
