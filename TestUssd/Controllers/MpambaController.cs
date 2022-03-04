using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.States.Mpamba;

namespace TestUssd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MpambaController : Controller
    {
        private readonly UssdContext _context;
        public MpambaController(IConnectionMultiplexer redis)
        {
            _context = new UssdContext(new RedisStore(redis), nameof(MpambaStartState));
        }
        [HttpPost("Ussd")]
        public async Task<UssdResponse> Ussd([FromBody] UssdRequest request)
        {
            return await _context.HandleRequest(request);
        }
    }
}
