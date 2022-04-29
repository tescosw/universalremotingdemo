using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TescoSW.DependencyInjection;
using TescoSW.Net.Http;
using TescoSW.OW.Remoting.Http;
using URDemo.TestBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.Add(ServiceDescriptor.Singleton<IHTTPCommunicator>((sc) => new SingleUserCommunicator(builder.HostEnvironment.BaseAddress)));
builder.Services.AddScopedOWAuthentication();
builder.Services.AddScopedOWManager();

await builder.Build().RunAsync();
