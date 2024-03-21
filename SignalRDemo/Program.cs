using Microsoft.OpenApi.Models;
using SignalRDemo.HostedServices;
using SignalRDemo.Hubs;
using SignalRDemo.Providers.Accounts;
using SignalRDemo.Providers.Balance;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SignalR Demo API",
        Version = "v1"
    });
});

#region Services
builder.Services.AddTransient<IBalanceProvider, BalanceProvider>(); 
builder.Services.AddTransient<IBalanceAsyncProvider, BalanceAsyncProvider>();
builder.Services.AddTransient<IAccountsProvider, AccountsProvider>();

builder.Services.AddSingleton<BalanceBackgroundService>();
builder.Services.AddHostedService<BalanceBackgroundService>();
#endregion

//добавяне на SignalR
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignalR Demo API");
    c.OAuthUsePkce();
    c.RoutePrefix = "v1/swagger";
});

//еднпийнта, на който слуша хъба
app.MapHub<BalanceHub>("/hub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
