namespace Colosus.Server.Services.Token
{
    public interface ITokenService
    {
        public string GenerateJwtToken(string userId, string name, List<string> roles);
        public bool ValidateJwtToken(ref string token);
        public string GetUserIDFromToken(string Token);
    }
}
