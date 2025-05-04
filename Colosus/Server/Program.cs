
using Colosus.Business;
using Colosus.Business.Abstracts;
using Colosus.Business.Concretes;
using Colosus.Operations.Abstracts;
using Colosus.Operations.Concretes;
using Colosus.Server.Facades.Administrator;
using Colosus.Server.Facades.Category;
using Colosus.Server.Facades.Customer;
using Colosus.Server.Facades.Firm;
using Colosus.Server.Facades.Login;
using Colosus.Server.Facades.Pos;
using Colosus.Server.Facades.Product;
using Colosus.Server.Facades.Setting;
using Colosus.Server.Services.Token;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

List<DataBaseSetting> dbSettingsSection = builder.Configuration.GetSection("DbSettings").Get<List<DataBaseSetting>>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()       // Herhangi bir domain'ye izin verir
                  .AllowAnyHeader()       // Herhangi bir header'a izin verir
                  .AllowAnyMethod();      // Herhangi bir HTTP metoduna izin verir
        });
});

#region Services

builder.Services.AddScoped<IOperationRunner, OperationRunner>((e) => {
    OperationRunner opRunner = new();
    opRunner.logEvent += (e) => { Console.WriteLine(e.ToString()); };
    return opRunner;
});
builder.Services.AddScoped<IHash, Sha256HashAlg>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IDataConverter, Json>();
builder.Services.AddScoped<IMapping, BasicMapping>();
builder.Services.AddScoped<IGuid, Colosus.Operations.Concretes.Guid>();
builder.Services.AddScoped<IOperations, Operations>((e) => {
    IGuid guidService = e.GetRequiredService<IGuid>();
    Operations returnedOperations = new();
    returnedOperations.AddDbSettings(dbSettingsSection);
    return returnedOperations;
});
#endregion
#region Facades
builder.Services.AddScoped<ICategoryFacades, CategoryFacades>();
builder.Services.AddScoped<IAdministratorFacades, AdministratorFacades>();
builder.Services.AddScoped<IFirmFacades, FirmFacades>();
builder.Services.AddScoped<ICategoryFacades, CategoryFacades>();
builder.Services.AddScoped<ILoginFacades, LoginFacades>();
builder.Services.AddScoped<IProductFacades, ProductFacades>();
builder.Services.AddScoped<ICustomerFacades, CustomerFacades>();
builder.Services.AddScoped<ISaleFacades, SaleFacades>();
builder.Services.AddScoped<ISettingFacades, SettingFacades>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
    var url = app.Configuration.GetValue("URLS", "").Split(";")[0] + "/swagger/index.html"; // portunu kontrol et!
    await Task.Run(() =>
    {
        // Küçük bir gecikme veriyoruz ki API tam otursun
        Thread.Sleep(2000);
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Swagger UI açýlýrken hata: {ex.Message}");
        }
    });

}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(); // Sunumdan sonra kapat

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
