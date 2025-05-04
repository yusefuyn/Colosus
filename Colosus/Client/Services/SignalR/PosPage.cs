using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Colosus.Client.Blazor.Services.SignalR
{

    public class PosPage
    {
        private readonly NavigationManager _navManager;
        public HubConnection? HubConnection { get; private set; }

        public event Action<List<SaleRequestModel>>? OnProductsUpdated;

        public PosPage(NavigationManager navManager)
        {
            _navManager = navManager;
        }

        public async Task ConnectAsync()
        {
            if (HubConnection is not null)
                return;

            HubConnection = new HubConnectionBuilder()
            .WithUrl(_navManager.ToAbsoluteUri("/salehub"))
                .WithAutomaticReconnect()
                .Build();

            HubConnection.On<List<SaleRequestModel>>("ReceiveProductList", products =>
            {
                OnProductsUpdated?.Invoke(products);
            });

            await HubConnection.StartAsync();
        }

        public async Task DisconnectAsync()
        {
            if (HubConnection is not null)
            {
                await HubConnection.StopAsync();
                await HubConnection.DisposeAsync();
                HubConnection = null;
            }
        }

        public async Task AddProduct(SaleRequestModel product)
        {
            if (HubConnection != null)
                await HubConnection.InvokeAsync("AddOrUpdateProduct", product);
        }

        public async Task RemoveProduct(string SaleRequestModelToken)
        {
            if (HubConnection != null)
                await HubConnection.InvokeAsync("RemoveProduct", SaleRequestModelToken);
        }

        public async Task Increase(string SaleRequestModelToken)
        {
            if (HubConnection != null)
                await HubConnection.InvokeAsync("IncreaseAmount", SaleRequestModelToken);
        }

        public async Task Decrease(string SaleRequestModelToken)
        {
            if (HubConnection != null)
                await HubConnection.InvokeAsync("DecreaseAmount", SaleRequestModelToken);
        }
    }
}
