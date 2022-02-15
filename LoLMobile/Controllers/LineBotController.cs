using isRock.LineBot;
using LoLMobile.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        private readonly string WelcomeMessage = @"歡迎新朋友 {0} 嗨你好

⭐️ 2 / 1 更新《 遊戲ID表單 》

請務必填寫Google表單留下ID，感謝大家配合。

方便日後我們知道誰是誰。

https://reurl.cc/6Ed7jZ

《公會》

群組已成立公會，公會搜尋 🔍 「 Gpride 」

想入的請直接申請然後告知我名字 ，請多多參與公會戰。

加入公會後，請務必武裝。

《Discord》

群組DC已成立，請務必多加利用。

iPhone 需要到設定裡找英雄聯盟的app 然後把麥克風關掉才行語音。

加入DC後記得把自己的名字改成遊戲名稱請到DC裡面點右下角頭像->帳戶->用戶名就可以更改囉。

https://ift.tt/3iczt1f

也請大家多多聊天，一起遊玩。";

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
                                                ReplyMessageWithJSON(@event.replyToken, new LineBotBll().Text(@event));
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
                                    string.Format(WelcomeMessage, string.Join(",", users))
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
                    JsonConvert.SerializeObject(receievedMessage)
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
