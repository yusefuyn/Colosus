using Colosus.Entity.Concretes;

namespace Colosus.Client.Services.Pos
{
    public interface IPosService
    {
        public Task<RequestResult> GetMyPosDTO(string firmPublicKey);
    }
}
