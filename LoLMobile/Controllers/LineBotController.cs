using isRock.LineBot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoLMobile.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        public IActionResult WebHook(ReceivedMessage receievedMessage)
        {
            return Ok();
        }
    }
}
