
using isRock.LineBot;
using LoLMobile.Bll;
using LoLMobile.Helper;

namespace LoLMobile.TimerTool
{
    public class LineBotTimer
    {
        public void StartTimer()
        {
            string funcName = string.Empty;
            try
            {
                funcName = "AnnouncementTimer";
                AnnouncementTimer();
                //funcName = "ActivityTimer";
                //ActivityTimer();
            }
            catch (Exception ex)
            {
                Utility.PushMessage(LineBotHelper.AdminId,
                    ex.Message +
                    "\n\r" +
                    funcName,
                    LineBotHelper.ChannelAccessToken
                );
            }

        }
        private void AnnouncementTimer()
        {
            DateTime dateTime = DateTime.Today;//00:00

            while (DateTime.Now > dateTime && DateTime.Now != dateTime)
            {
                dateTime = dateTime.AddHours(6);//06:00,12:00,18:00
            }

            int UntiFour = (int)((dateTime - DateTime.Now).TotalMilliseconds);
            Timer timer = new(GetAnnouncement);
            timer.Change(UntiFour, Timeout.Infinite);
        }

        private void GetAnnouncement(object? state)
        {
            string message = new GoogleBll().GetAnnouncement();

            if (!LineBotHelper.CheckMessageLength(message))
            {
                message = "公告訊息格式錯誤";
            }
            Utility.PushMessage(LineBotHelper.GroupId, message, LineBotHelper.ChannelAccessToken);
            AnnouncementTimer();
        }

        private void ActivityTimer()
        {
            DateTime dateTime = DateTime.Today.AddHours(3);//03:00

            while (DateTime.Now > dateTime && DateTime.Now != dateTime)
            {
                dateTime = dateTime.AddHours(6);//09:00,15:00,21:00
            }

            int UntiFour = (int)(dateTime - DateTime.Now).TotalMilliseconds;
            Timer timer = new(GetActivity);
            timer.Change(UntiFour, Timeout.Infinite);
        }

        private void GetActivity(object? state)
        {
            string message = new GoogleBll().GetAnnouncement();

            if (!LineBotHelper.CheckMessageLength(message))
            {
                Utility.PushMessage(LineBotHelper.GroupId, "活動訊息格式錯誤", LineBotHelper.ChannelAccessToken);
            }
            Utility.PushMessage(LineBotHelper.GroupId, message, LineBotHelper.ChannelAccessToken);
            ActivityTimer();
        }
    }
}
