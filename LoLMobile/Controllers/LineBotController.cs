using isRock.LineBot;
using LoLMobile.Bll;
using LoLMobile.Extension;
using LoLMobile.Helper;
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
            ChannelAccessToken = LineBotHelper.ChannelAccessToken;
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
                                            TextReply(@event,@event.message.text);
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
                        case "postback":
                            {
                                //TextReply(@event,@event.postback.data);
                                Test();
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

        private void TextReply(Event @event,string text)
        {
            Regex regex = new(@"^#");
            if (regex.IsMatch(text))
            {
                string message = new LineBotBll().Text(@event, text);
                Func<string, string, string> func = ReplyMessage;
                if (message.IsJsonArrayValid())
                {
                    func = ReplyMessageWithJSON;
                }
                func(@event.replyToken, message);
            }
        }


        [HttpPost]
        public string Test()
        {
           return new LineBotBll().GetActivityUser(new Event
            {
                message = new Message
                {
                    text = "#測試"
                },
            }, "#活動用戶資訊 : 進擊の峽谷團👨🏼‍🤝‍👨🏻（台南篇）"
            );
        }
    }
}
