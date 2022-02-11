using isRock.LineBot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoLMobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        public IActionResult Hook(ReceivedMessage receievedMessage)
        {
            return Ok();
        }
    }
}
