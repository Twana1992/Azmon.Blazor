using Azmon.Client;
using Azmon.Client.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7103/") });
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<Cus_PayService>();
builder.Services.AddScoped<SellService>();
builder.Services.AddScoped<SellDetailService>();
builder.Services.AddScoped<ProductService>();
await builder.Build().RunAsync();
