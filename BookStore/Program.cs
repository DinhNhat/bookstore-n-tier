using System.Text.Json.Serialization;
using BookStore.HelperExtensions;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AdminServices;
using ServiceLayer.AdminServices.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Entity Framework Core DbContext configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' for database book store not found.");
builder.Services.AddDbContext<EfCoreContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddTransient<IChangePubDateService, ChangePubDateService>();
builder.Services.AddTransient<IChangePriceOfferService, ChangePriceOfferService>();

var app = builder.Build();

// Create a temporary scope to resolve services for initialization
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.SetupDatabaseAsync();
}

app.UseStaticFiles();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();
