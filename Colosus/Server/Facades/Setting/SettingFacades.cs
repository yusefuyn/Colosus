using Colosus.Business.Abstracts;
using Colosus.Operations.Abstracts;
using Colosus.Server.Services.Token;

namespace Colosus.Server.Facades.Setting
{
    public class SettingFacades : ISettingFacades
    {
        public SettingFacades(IDataConverter dataConverter, IDatabaseOperations operations, IOperationRunner operationRunner, ITokenService tokenService, IGuid guid, IMapping mapping)
        {
            this.dataConverter = dataConverter;
            this.operations = operations;
            this.operationRunner = operationRunner;
            this.tokenService = tokenService;
            this.guid = guid;
            this.mapping = mapping;
        }

        public IDataConverter dataConverter {get;set; }
        public IDatabaseOperations operations {get;set; }
        public IOperationRunner operationRunner {get;set; }
        public ITokenService tokenService {get;set; }
        public IGuid guid {get;set; }
        public IMapping mapping { get; set; }
    }
}
