using GrpcEchoService.Services;
using EchoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<DataModels.Interfaces.IEchoService>(new EchoService.Services.EchoService());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcEchoService.Services.EchoService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
