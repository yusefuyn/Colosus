using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Pos
{
    public class PosService : IPosService
    {
        HttpClientService clientService;
        public PosService(HttpClientService clientService)
        {
            this.clientService = clientService;
        }

        public string GetAddress(string Action) => AppState.GetAddress("Pos",Action);

        public Task<RequestResult> GetMyPosDTO(string firmPublicKey)
            => clientService.GetPostAsync<RequestResult>(firmPublicKey, GetAddress("GetMyPosDTO"));
    }
}
