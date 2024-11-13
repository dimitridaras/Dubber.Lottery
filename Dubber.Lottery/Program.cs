using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dubber.Lottery;
using Dubber.Lottery.DrawGeneration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// DI - add services here
// DrawGenerator contains state, so it may as well be singleton in this SPA app.
builder.Services.AddSingleton<IDrawGenerator, DrawGenerator>();

// could register a singleton DrawConfuration class here, taken from appSettings.json.
// this could then be a dependency of the DrawGenerator

await builder.Build().RunAsync();

