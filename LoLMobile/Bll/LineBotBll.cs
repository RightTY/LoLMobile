using Google.Apis.Sheets.v4.Data;
using isRock.LineBot;
using LoLMobile.Helper;
using LoLMobile.Models.Line.FlexMessage;
using LoLMobile.Models.Line.FlexMessage.Element;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace LoLMobile.Bll
{
    public class LineBotBll
    {
        public string Text(Event @event)
        {
            Dictionary<Regex, Func<Event, string>> dic = new()
            {
                { new Regex("^#成員名單"), GetUsers },//官網
            };

            var Funcs = dic.Where(x => x.Key.IsMatch(@event.message.text) == true);

            if (!Funcs.Any())
            {
                return "查無關鍵字";
            }
            else
            {
                return Funcs.First().Value(@event);
            }
        }

        public string GetUsers(Event @event)
        {
            GoogleBll googleBll = new();
            ValueRange valueRange = googleBll.GetUsers();

            Flexmessage flexmessage = new()
            {
                Type = "flex",
                AltText = "成員名單",
                Contents = new Carousel
                {
                    Type = "carousel"
                }
            };

            int maxCount = 10;
            bool isDivisible = !(valueRange.Values.Count % maxCount > 0);
            int frequency = isDivisible ? valueRange.Values.Count / maxCount : (valueRange.Values.Count / maxCount + 1);
            List<Bubble> bubbles = new List<Bubble>();
            for (int i = 0; i < frequency; i++)
            {
                var values = valueRange.Values.Skip(i * maxCount).Take(maxCount);
                Bubble bubble = new()
                {
                    Type = "bubble",
                    Body = new FlexBody
                    {
                        Type = "box",
                        Layout = "vertical",
                        Contents = new List<Content>()
                    }
                };

                List<Content> boxContents = new()
                {
                    new Content
                    {
                        Type = "box",
                        Layout = "baseline",
                        Spacing = "md",
                        Contents = new()
                        {
                            new Content
                            {
                                Type = "text",
                                Text = "Line暱稱",
                                Size = "sm",
                                Color = "#aaaaaa",
                                Align = "center"
                            },
                            new Content
                            {
                                Type = "text",
                                Text = "角色暱稱",
                                Size = "sm",
                                Color = "#aaaaaa",
                                Align = "center"
                            },
                            new Content
                            {
                                Type = "text",
                                Text = "居住地區",
                                Size = "sm",
                                Color = "#aaaaaa",
                                Align = "center"
                            },
                            new Content
                            {
                                Type = "text",
                                Text = "生日",
                                Size = "sm",
                                Color = "#aaaaaa",
                                Align = "center"
                            }
                        }
                    }
                };
                List<Content> dataBoxContents = new List<Content>(); ;
                foreach (object item in values)
                {
                    List<object> datas = item as List<object>;
                    dataBoxContents.Add(new Content
                    {
                        Type = "box",
                        Layout = "baseline",
                        Spacing = "md",
                        Margin = "lg",
                        Contents = new()
                        {
                            new Content
                            {
                                Type = "text",
                                Text = datas[0].ToString(),
                                Size = "sm",
                                Color = "#666666",
                                Align = "center",
                                Wrap = true
                            },
                            new Content
                            {
                                Type = "text",
                                Text = datas[1].ToString(),
                                Size = "sm",
                                Color = "#666666",
                                Align = "center",
                                Wrap = true
                            },
                            new Content
                            {
                                Type = "text",
                                Text = datas[2].ToString(),
                                Size = "sm",
                                Color = "#666666",
                                Align = "center",
                                Wrap = true
                            },
                            new Content
                            {
                                Type = "text",
                                Text = datas[3].ToString(),
                                Size = "sm",
                                Color = "#666666",
                                Align = "center"
                            }
                        }
                    });
                }

                List<Content> dataContent = boxContents.Concat(dataBoxContents).ToList();
                bubble.Body.Contents = bubble.Body.Contents.Concat(dataContent).ToList();
                bubbles.Add(bubble);
            }
            flexmessage.Contents.Contents = bubbles.ToList();
            List<Flexmessage> flexmessages = new() { flexmessage };

            return JsonConvert.SerializeObject(
                flexmessages,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }
    }
}
