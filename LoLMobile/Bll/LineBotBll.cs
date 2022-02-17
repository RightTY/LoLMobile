using Google.Apis.Sheets.v4.Data;
using isRock.LineBot;
using LoLMobile.Helper;
using LoLMobile.Models;
using LoLMobile.Models.Line.FlexMessage;
using LoLMobile.Models.Line.FlexMessage.Element;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LoLMobile.Bll
{
    public class LineBotBll
    {
        public string Text(Event @event, string text)
        {
            Dictionary<Regex, Func<Event, string, string>> dic = new()
            {
                { new Regex("^#成員名單"), GetUsers },//人員名單
                { new Regex("^#公告"), GetAnnouncement },//公告
                { new Regex("^#取得Uid"), GetUserUid },//取得Uid
                { new Regex("^#活動資訊"), GetActivity },//取得活動資訊
                { new Regex("^#活動用戶資訊"), GetActivityUser }//取得活動成員資訊
            };

            var Funcs = dic.Where(x => x.Key.IsMatch(text) == true);

            if (!Funcs.Any())
            {
                return "無此功能";
            }
            else
            {
                return Funcs.First().Value(@event, text);
            }
        }

        public string GetUsers(Event @event, string text)
        {
            GoogleBll googleBll = new();
            ValueRange valueRange = googleBll.GetUsers();

            Flexmessage flexmessage = new()
            {
                Type = "flex",
                AltText = "成員名單",
            };

            int maxCount = 10;
            bool isDivisible = !(valueRange.Values.Count % maxCount > 0);
            int frequency = isDivisible ? valueRange.Values.Count / maxCount : (valueRange.Values.Count / maxCount + 1);
            List<Bubble> bubbles = new();
            for (int i = 0; i < frequency; i++)
            {
                var values = valueRange.Values.Skip(i * maxCount).Take(maxCount);
                Bubble bubble = new()
                {
                    Body = new FlexBody
                    {
                        Type = "box",
                        Layout = "vertical"
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
                List<Content> dataBoxContents = new(); ;
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
            return JsonSerializer.Serialize(
                flexmessages,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }
            );
        }

        public string GetAnnouncement(Event @event, string text)
        {
            return new GoogleBll().GetAnnouncement();
        }

        public string GetUserUid(Event @event, string text)
        {
            if (@event.source.userId is not null)
            {
                return @event.source.userId;
            }
            return "請私聊使用此功能";
        }

        public string GetActivity(Event @event, string text)
        {
            GoogleBll googleBll = new();
            ValueRange valueRange = googleBll.GetActivity();

            Flexmessage flexmessage = new()
            {
                Type = "flex",
                AltText = "活動資訊"
            };

            List<Bubble> bubbles = new();

            foreach (IList<object> item in valueRange.Values)
            {
                Bubble bubble = new()
                {
                    Body = new FlexBody
                    {
                        Type = "box",
                        Layout = "vertical",
                        Contents = new List<Content>
                        {
                            new Content
                            {
                                Type ="text",
                                Text = item[2].ToString(),
                                Weight = "bold",
                                Size = "lg",
                                Wrap = true
                            },
                            new Content
                            {
                                Type = "box",
                                Layout = "vertical",
                                Margin = "lg",
                                Spacing = "sm",
                                Contents = new()
                                {
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Contents = new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "活動時間",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[3].ToString() + "\n " + item[4].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    },
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Margin = "md",
                                        Contents = new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "報名時間",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[0].ToString() + "\n " + item[1].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    },
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Margin = "md",
                                        Contents = new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "報名方式",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[7].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    },
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Margin = "md",
                                        Contents = new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "活動內容",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[5].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    },
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Margin = "md",
                                        Contents= new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "行程",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[8].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    },
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Margin = "md",
                                        Contents= new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "住宿",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[9].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    },
                                    new Content
                                    {
                                        Type ="box",
                                        Layout = "baseline",
                                        Spacing = "sm",
                                        Margin = "md",
                                        Contents= new()
                                        {
                                            new Content
                                            {
                                                Type = "text",
                                                Text = "其他",
                                                Color = "#aaaaaa",
                                                Size = "sm",
                                                Flex = 2
                                            },
                                            new Content
                                            {
                                                Type = "text",
                                                Text = item[6].ToString(),
                                                Wrap = true,
                                                Color = "#666666",
                                                Size = "sm",
                                                Flex = 5
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Footer = new FlexFooter
                    {
                        Type = "box",
                        Layout = "vertical",
                        Spacing = "sm",
                        Contents = new List<Content> {
                            new Content
                            {
                                Type = "button",
                                Style = "link",
                                Height = "sm",
                                Action = new FlexAction
                                {
                                    Type = "postback",
                                    Label = "成員名單",
                                    Data = "#活動用戶資訊 : " + item[2].ToString(),

                                }
                            }
                        }
                    }
                };
                bubbles.Add(bubble);
            }
            flexmessage.Contents.Contents = bubbles.ToList();
            List<Flexmessage> flexmessages = new() { flexmessage };
            return JsonSerializer.Serialize(
                flexmessages,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }
            );
        }

        public string GetActivityUser(Event @event, string text)
        {
            string activityName = text.Replace("#活動用戶資訊 : ", string.Empty);
            GoogleBll googleBll = new();
            ValueRange valueRange = googleBll.GetActivityUser(activityName);

            Flexmessage flexmessage = new()
            {
                Type = "flex",
                AltText = "成員名單",
            };

            if (valueRange.Values is not null)
            {
                int maxCount = 10;
                bool isDivisible = !(valueRange.Values.Count % maxCount > 0);
                int frequency = isDivisible ? valueRange.Values.Count / maxCount : (valueRange.Values.Count / maxCount + 1);
                List<Bubble> bubbles = new();
                for (int i = 0; i < frequency; i++)
                {
                    var values = valueRange.Values.Skip(i * maxCount).Take(maxCount);
                    Bubble bubble = new()
                    {
                        Body = new FlexBody
                        {
                            Type = "box",
                            Layout = "vertical",

                        }
                    };

                    List<Content> boxContents = new()
                    {
                        new Content
                        {
                            Type = "text",
                            Text = activityName,
                            Wrap = true
                        },
                        new Content
                        {
                            Type = "box",
                            Layout = "baseline",
                            Spacing = "sm",
                            Contents = new()
                            {
                                new Content
                                {
                                    Type = "text",
                                    Text = "點擊者",
                                    Color = "#aaaaaa",
                                    Size = "sm",
                                    Flex = 2
                                },
                                new Content
                                {
                                    Type = "text",
                                    Text = Utility.GetGroupMemberProfile(LineBotHelper.GroupId, @event.source.userId, LineBotHelper.ChannelAccessToken).displayName,
                                    Wrap = true,
                                    Color = "#666666",
                                    Size = "sm",
                                    Flex = 5
                                }
                            }
                        },
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
                                    Text = "參加時間",
                                    Size = "sm",
                                    Color = "#aaaaaa",
                                    Align = "center"
                                },
                                new Content
                                {
                                    Type = "text",
                                    Text = "住宿情況",
                                    Size = "sm",
                                    Color = "#aaaaaa",
                                    Align = "center"
                                },
                                new Content
                                {
                                    Type = "text",
                                    Text = "備註",
                                    Size = "sm",
                                    Color = "#aaaaaa",
                                    Align = "center"
                                }
                            }
                        }
                    };
                    List<Content> dataBoxContents = new(); ;
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
                                    Align = "center",
                                    Wrap = true
                                }
                            }
                        });
                    }

                    List<Content> dataContent = boxContents.Concat(dataBoxContents).ToList();
                    bubble.Body.Contents = bubble.Body.Contents.Concat(dataContent).ToList();
                    bubbles.Add(bubble);
                }
                flexmessage.Contents.Contents = bubbles.ToList();
            }

            List<Flexmessage> flexmessages = new() { flexmessage };
            return JsonSerializer.Serialize(
                flexmessages,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }
            );
        }
    }
}
