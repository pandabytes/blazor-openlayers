using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.OpenLayers.Sample;
using Blazor.OpenLayers;
using Blazor.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddOpenLayers()
  .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var webHost = builder
  .Build()
  .ConfigureIJSRuntimeJsonOptions();

await webHost.Services.RegisterAttachReviverAsync();
await webHost.RunAsync();
