using Colosus.Client.Blazor.Components;
using Colosus.Client.Blazor.Layouts;
using Colosus.Client.Blazor.Services;
using Colosus.Entity.Concretes;

namespace Colosus.Client
{
    public static class AppState
    {
        private static string _selectedFirmPublicKey;


        public static CookieService cookieService { get; set; }

        public static Notification notificationComponent;
        public static string GetAddress(string Controller, string Action) => $"Api/{Controller}/{Action}";
        public static void AddMessage(string str) => notificationComponent.AddMessage(str);

        public static event Action OnSelectedFirmPublicKeyChanged;

        public static async Task<string> GetUserPublicKey()
        {
            return await cookieService.GetCookie("userpublickey");
        }

        public static async Task SetUserPublicKey(string value)
        {
            await cookieService.SetCookie("userpublickey", value, 60);
        }

        public static async Task<string> GetUserName()
        {
            return await cookieService.GetCookie("username");
        }

        public static async Task SetUserName(string value)
        {
            await cookieService.SetCookie("username", value, 60);
        }

        public static async Task<string> GetToken()
        {
            return await cookieService.GetCookie("Token");
        }

        public static async Task SetToken(string value)
        {
            await cookieService.SetCookie("Token", value, 60);
        }

        public static string SelectedFirmPublicKey
        {
            get => _selectedFirmPublicKey;
            set
            {
                if (_selectedFirmPublicKey != value)
                {
                    _selectedFirmPublicKey = value;
                    OnSelectedFirmPublicKeyChanged?.Invoke();
                }
            }
        }

        public static string SelectedFirmName { get; set; } = "";
        public static string ConvertBoolToIcon(bool val)
        {
            if (val) return "fa fa-check";
            else return "fa fa-close";
        }
    }
}
