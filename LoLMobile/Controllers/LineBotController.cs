using isRock.LineBot;
using LoLMobile.Bll;
using LoLMobile.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LoLMobile.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineBotController : LineWebHookControllerBase
    {
        public LineBotController()
        {
            ChannelAccessToken = "SU0mi6z1Lt02IOa2sxSMYfhO+X/yFMfaTxcMH4QTqjHaXK8KEYPv6WH0ohvFf2pOElwsNyjhg9gLvhstfweDHnguKk4zqOFYC7QMgj8G5ZSc01PZWhKDUMeddu4sLaiSZ5xNSwy9xUqp8YRM0TlsGwdB04t89/1O/w1cDnyilFU=";
        }

        [HttpPost]
        public IActionResult WebHook([FromBody] ReceivedMessage receievedMessage)
        {
            try
            {
                foreach (Event @event in receievedMessage.events)
                {
                    switch (@event.type)
                    {
                        case "message":
                            {
                                switch (@event.message.type)
                                {
                                    case "text":
                                        {
                                            Regex regex = new(@"^#");
                                            if (regex.IsMatch(@event.message.text))
                                            {
                                                string message = new LineBotBll().Text(@event);
                                                Func<string, string, string> func = ReplyMessage;
                                                if (message.IsJsonArrayValid())
                                                {
                                                    func = ReplyMessageWithJSON;
                                                }
                                                func(@event.replyToken, message);
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case "memberJoined":
                            {
                                List<string> users = new();
                                foreach (SourceUser sourceUser in @event.joined.members)
                                {
                                    LineUserInfo lineUserInfo = Utility.GetGroupMemberProfile(@event.source.groupId, sourceUser.userId, ChannelAccessToken);
                                    users.Add(lineUserInfo.displayName);
                                }
                                ReplyMessage(@event.replyToken, new TextMessage(
                                    string.Format(new GoogleBll().GetWelcomeMsg(), string.Join(",", users))
                                ));
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                PushMessage("U614a3caf28092783f99e50ccd5372567",
                    ex.Message +
                    "\n\r" +
                    JsonSerializer.Serialize(receievedMessage)
                );
            }
            return Ok();
        }



        [HttpPost]
        public string Test()
        {
            return new LineBotBll().GetUsers(new Event());
        }
    }
}
