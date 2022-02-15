using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Threading.Tasks;
using TestUssd.Core;
using TestUssd.State;

namespace TestUssd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly UssdContext _context;
        public GameController(IConnectionMultiplexer redis)
        {
            _context = new UssdContext(new RedisStore(redis), nameof(StartState));
        }
        [HttpPost("Ussd")]
        public async Task<UssdResponse> Ussd([FromBody] UssdRequest request)
        {
            return await _context.HandleRequest(request);
        }
    }
}
