using Colosus.Operations.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colosus.Operations.Concretes
{
    public class OperationRunner : IOperationRunner
    {
        public event IOperationRunner.LogEvent logEvent;
        public void ActionRunner(Action firstAct, Action errorAct)
        {
			try
			{
                firstAct.Invoke();
            }
            catch (Exception ex)
            {
                errorAct.Invoke();
                logEvent?.Invoke($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}, Date: {DateTime.Now}");
            }
        }
    }
}
