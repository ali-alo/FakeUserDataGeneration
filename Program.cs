using FakeUserDataGeneration;
using FakeUserDataGeneration.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<UsersGenerationService>();
builder.Services.AddSingleton<ErrorGenerationService>();

await builder.Build().RunAsync();
