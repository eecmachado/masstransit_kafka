using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.AddApplicationBuilder();

app.Run();
