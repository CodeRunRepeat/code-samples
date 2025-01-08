using Microsoft.AspNetCore.Mvc;
using DataModels;
using DataModels.Interfaces;

namespace AspnetEchoService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {
        private readonly ILogger<EchoController> _logger;
        private readonly IEchoService _echoservice;

        public EchoController(ILogger<EchoController> logger, IEchoService echoService)
        {
            _logger = logger;
            _echoservice = echoService;
        }

        [HttpGet(Name = "Echo")]
        public EchoResponse Get(string content)
        {
            return _echoservice.Echo(content);
        }
    }
}
