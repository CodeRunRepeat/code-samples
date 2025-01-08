using DataModels;
using DataModels.Interfaces;

namespace EchoService.Services
{
    public class EchoService : IEchoService
    {
        public EchoResponse Echo(string request)
        {
            return new EchoResponse(request, (request ?? "").Length);
        }
    }
}
