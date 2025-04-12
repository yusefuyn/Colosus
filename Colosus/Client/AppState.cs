using Colosus.Client.Blazor.Components;
using Colosus.Client.Blazor.Layouts;
using Colosus.Entity.Concretes;

namespace Colosus.Client
{
    public static class AppState
    {
        public static Notification notificationComponent;
        public static string GetAddress(string Controller, string Action) => $"Api/{Controller}/{Action}";
        public static void AddMessage(string str) => notificationComponent.AddMessage(str);

        public static event Action OnSelectedFirmPublicKeyChanged;
        private static string _selectedFirmPublicKey;

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
