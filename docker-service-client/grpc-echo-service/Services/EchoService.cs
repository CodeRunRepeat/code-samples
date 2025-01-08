using Grpc.Core;
using GrpcEchoService.Protos;
using DataModels.Interfaces;

namespace GrpcEchoService.Services;
public class EchoService : Echo.EchoBase
{
    private readonly ILogger<EchoService> _logger;
    private readonly IEchoService _echoService;

    public EchoService(ILogger<EchoService> logger, IEchoService echoService)
    {
        _logger = logger;
        _echoService = echoService;   
    }

    public override Task<EchoResponse> Echo(EchoRequest request, ServerCallContext context)
    {
        return Task.FromResult(_echoService.Echo(request.Content).ToProtobuf());

    }
}
