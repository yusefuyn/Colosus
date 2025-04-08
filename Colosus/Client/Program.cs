using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Colosus.Client;
using Colosus.Operations.Abstracts;
using Colosus.Operations.Concretes;
using Colosus.Client.Services;
using Colosus.Client.Services.Login;
using Microsoft.AspNetCore.Components.Authorization;
using Colosus.Client.Services.Firm;
using Colosus.Client.Services.Administrator;
using Colosus.Client.Services.Category;
using Colosus.Client.Services.Product;
using Colosus.Client.Services.Customer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Colosus.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Colosus.ServerAPI"));
builder.Services.AddScoped<IHash, Sha256HashAlg>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<IDataConverter, Json>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFirmService, FirmService>();
builder.Services.AddScoped(sp => {
    HttpClient returned = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    return returned;
});
builder.Services.AddScoped<HttpClientService>();


builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();
