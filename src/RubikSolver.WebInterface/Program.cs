using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RubikSolver.WebInterface;
using RubikSolver.WebInterface.Data.Services;
using RubikSolver.WebInterface.Data.Services.Interfaces;
using RubikSolver.WebInterface.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<CubeSolverApiOptions>(builder.Configuration.GetSection(CubeSolverApiOptions.Section).Bind);
builder.Services.AddTransient<ICube2X2Service, Cube2X2Service>();

await builder.Build().RunAsync();