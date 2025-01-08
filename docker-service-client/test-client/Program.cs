using Grpc.Net.Client;
using GrpcEchoService.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestClient;

try
{
    var builder = Host.CreateApplicationBuilder(args);

    using IHost host = builder.Build();

    CallOpenApiService(host);
    CallGrpcService(host);

    await host.RunAsync();
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
}

static void CallGrpcService(IHost host)
{
    IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
    var grpcUrl = config.GetValue<string>("GrpcUrl");

    Console.WriteLine($"Calling gRPC service at {grpcUrl}");
    using var channel = GrpcChannel.ForAddress(grpcUrl ?? "");

    var client = new Echo.EchoClient(channel);
    var response = client.Echo(new EchoRequest{ Content = "Hello, world" });

    Console.WriteLine(response);
}

static void CallOpenApiService(IHost host)
{
    IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
    var baseUrl = config.GetValue<string>("BaseUrl");

    Console.WriteLine($"Calling OpenAPI service at {baseUrl}");

    using var httpClient = new HttpClient();
    var client = new EchoServiceClient(httpClient);
    client.BaseUrl = baseUrl ?? client.BaseUrl;

    client.GetAsync("Hello, world").
        ContinueWith(completed => Console.WriteLine($"Content={completed.Result.Content}, Count={completed.Result.Count}")).
        Wait();
}
