using ElectronicMediaAPI;
using ElectronicMedia.Core;
using ElectronicMedia.Core.Automaper;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
AutomapperCore.Init(app.Services);
startup.Configure(app, builder.Environment);
