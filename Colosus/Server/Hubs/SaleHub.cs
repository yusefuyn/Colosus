using Colosus.Entity.Concretes.DTO;
using Colosus.Entity.Concretes.RequestModel;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Colosus.Server.Hubs
{
    public class SaleHub : Hub
    {

        private static ConcurrentDictionary<string, SaleRequestModel> Products = new();

        // Tüm client’lara mevcut listeyi gönder
        private async Task BroadcastProductList()
        {
            await Clients.All.SendAsync("ReceiveProductList", Products.Values.ToList());
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            // Bağlanan kullanıcıya mevcut ürün listesini gönder
            await Clients.Caller.SendAsync("ReceiveProductList", Products.Values.ToList());
        }

        public async Task AddOrUpdateProduct(SaleRequestModel product)
        {
            Products.AddOrUpdate(product.SaleRequestModelToken, product, (key, existing) =>
            {
                existing.SalesAmount += product.SalesAmount;
                return existing;
            });

            await BroadcastProductList();
        }

        public async Task RemoveProduct(string SaleRequestModelToken)
        {
            Products.TryRemove(SaleRequestModelToken, out _);
            await BroadcastProductList();
        }

        public async Task IncreaseAmount(string SaleRequestModelToken)
        {
            if (Products.TryGetValue(SaleRequestModelToken, out var product))
            {
                product.SalesAmount++;
                await BroadcastProductList();
            }
        }

        public async Task DecreaseAmount(string SaleRequestModelToken)
        {
            if (Products.TryGetValue(SaleRequestModelToken, out var product) && product.SalesAmount > 0)
            {
                product.SalesAmount--;
                await BroadcastProductList();
            }
        }
    }
}
