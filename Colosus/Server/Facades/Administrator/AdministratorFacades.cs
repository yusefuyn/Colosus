using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;

namespace Colosus.Server.Facades.Administrator
{
    public class AdministratorFacades : IAdministratorFacades
    {

        public AdministratorFacades(IDataConverter dataConverter, IHash hash, IDatabaseOperations operations, IOperationRunner operationRunner, IGuid guid)
        {
            this.dataConverter = dataConverter;
            this.hash = hash;
            this.operations = operations;
            this.operationRunner = operationRunner;
            this.guid = guid;
        }

        public IDataConverter dataConverter { get;set; }
        public IHash hash { get;set; }
        public IDatabaseOperations operations { get;set; }
        public IOperationRunner operationRunner { get;set; }
        public IGuid guid { get;set; }
    }
}
