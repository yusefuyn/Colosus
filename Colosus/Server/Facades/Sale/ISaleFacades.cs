using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Pos
{
    public interface ISaleFacades
    {

        IDataConverter dataConverter { get; set; }
        IOperations operations { get; set; }
        IGuid guid { get; set; }
        IOperationRunner operationRunner { get; set; }
        IMapping mapping { get; set; }

    }
}
