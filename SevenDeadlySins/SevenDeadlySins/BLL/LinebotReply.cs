using isRock.LineBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SevenDeadlySins.Models.Line;
using SevenDeadlySins.Models.Line.LineBotMessage;
using SevenDeadlySins.Models.Line.LineBotMessage.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SevenDeadlySins.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace SevenDeadlySins.BLL
{
    public class LinebotReply : LineBotBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IOptions<Setting> _settings;
        /// <summary>
        /// 
        /// </summary>
        private ImgurBLL imgurBLL;
        private string type = "text";
        private IHttpClientFactory _clientFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public LinebotReply(IOptions<Setting> settings, IHttpClientFactory clientFactory)
        {
            _settings = settings;
            _clientFactory = clientFactory;
            SetBot("ZzUoNDYFET8fh5Zo1p3Pnhumom8u5xasHsW4rrK1nnWbpr0HE9Ao1a8ESVMUVjANf4NWf1yIy3HXalPcdIOynOf28xcrPaeSFmlGM9ZdHnkf/8LOEtaTiTNiYTuKZ7OhRCnEODyWF3Kh3sfxBww/jQdB04t89/1O/w1cDnyilFU=//mr3nBzuMfhkB8zXKc6L5zqSact4nvpEACZdC6DjQdB04t89/1O/w1cDnyilFU=");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        public void Text(Event eventData)
        {
            Dictionary<Regex, Func<Event, string>> dic = new Dictionary<Regex, Func<Event, string>>()
            {
                #region 暫時無資料庫使用
                //{ new Regex("^#\u6392\u968a\u52a0\u5165"),QueueAdd},//排隊加入
                //{ new Regex("^#\u5831\u5230"),AddMember},//報到
                //{ new Regex("^#\u6211\u627e"),SelectMember},//誰是
                //{ new Regex("^#\u65b0\u589e\u9a0e\u58eb\u5718"),AddGuild},//新增騎士團
                //{ new Regex("^#\u9a0e\u58eb\u5718\u6210\u54e1"),SelectGuildMember},//騎士團成員
                //{ new Regex("^#\u65b0\u589e"),AddKeyWord},//新增關鍵字
                #endregion 暫時無資料庫使用
                { new Regex("^#\u5b98\u7db2"),OfficialWebsite},//官網
                { new Regex("^#\u5df4\u54c8\u6587\u7ae0"),GamerWebsite},//巴哈文章
                { new Regex("^#\u6700\u65b0\u516c\u544a"),OfficialWebsiteNew}//最新公告
            };
            var replyMessageFun = dic.FirstOrDefault(x => x.Key.IsMatch(eventData.message.text) == true).Value;
            string replyMessage = replyMessageFun == null ? "None Data" : replyMessageFun(eventData);
            #region 暫時無資料庫使用
            //if (replyMessageFun == null)
            //{
            //    Regex regex = new Regex("^#");//關鍵字
            //    replyMessage = regex.IsMatch(eventData.message.text) ? SelectKeyWord(eventData) : replyMessage;
            //}
            #endregion 暫時無資料庫使用
            Reply(eventData, replyMessage, type);
        }
        public string GamerWebsite(Event eventData)
        {
            return "https://forum.gamer.com.tw/B.php?bsn=34342";
        }
        public string OfficialWebsite(Event eventData)
        {
            return "http://forum.netmarble.com/7ds_tw";
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="eventData"></param>
        public void Image(Event eventData)
        {
            imgurBLL = new ImgurBLL(_settings, _clientFactory);
            byte[] imageArray = GetImage(eventData);
            using Stream stream = new MemoryStream(imageArray);
            imgurBLL.image = imgurBLL.UploadImageAsync(stream);
            //TextMessage textMsg = new TextMessage(imgurBLL.image.Link);
            //ImageMessage imageMsg = new ImageMessage(new Uri(imgurBLL.image.Link), new Uri(imgurBLL.image.Link));
            //List<MessageBase> messages = new List<MessageBase>
            //{
            //    textMsg,
            //    imageMsg
            //};
            //ReplyMessages(eventData, messages);
        }
        /// <summary>
        /// 官網公告爬蟲
        /// </summary>
        /// <returns></returns>
        public string OfficialWebsiteNew(Event eventData)
        {
            List<JObject> jObjectData = new List<JObject>();
            string[] nums = { "1", "3", "8", "12" };
            HttpClient httpClient = _clientFactory.CreateClient();
            foreach (string num in nums)
            {
                string jsonStr = httpClient.GetStringAsync(@"http://forum.netmarble.com/api/game/nanagb/official/forum/7ds_tw/article/list?menuSeq=" + num + @"&viewType=lv&start=0&rows=4&sort=NEW").GetAwaiter().GetResult();
                jObjectData.Add(JObject.Parse(jsonStr));
            }

            List<Bubble> bubbles = new List<Bubble>();
            int jObjectDataIndex = 0;
            foreach (JObject jObject in jObjectData)
            {
                jObjectDataIndex++;
                Bubble bubble = new Bubble
                {
                    Type = "bubble",
                    Body = new FlexBody
                    {
                        Type = "box",
                        Layout = "vertical",
                        Contents = new Content[]
                        {
                            new Content
                            {
                                Type = "text",
                                Text = GetTitle(jObjectDataIndex),
                                Weight = "bold",
                                Size = "xl"
                            }
                        }
                    },
                    Footer = new FlexFooter
                    {
                        Type = "box",
                        Layout = "vertical"
                    }
                };

                List<Content> contents = new List<Content>();
                int articleIndex = 0;
                foreach (JObject article in jObject["articleList"] as JArray)
                {
                    articleIndex++;
                    contents.Add(
                        new Content
                        {
                            Type = "button",
                            Style = "link",
                            Height = "sm",
                            Action = new FlexAction
                            {
                                Type = "uri",
                                Label = article["title"].ToString()
                                .Substring(
                                    0,
                                    article["title"].ToString().Length > 40 ? 39 : article["title"].ToString().Length
                                ),
                                Uri = new Uri("http://forum.netmarble.com/7ds_tw/view/1/" + article["id"].ToString())
                            }
                        }
                    );

                    if (articleIndex != (jObject["articleList"] as JArray).Count)
                    {
                        contents.Add(
                            new Content
                            {
                                Type = "separator"
                            }
                        );
                    }

                };
                bubble.Footer.Contents = contents.ToArray();
                bubbles.Add(bubble);
            }

            Carousel carousel = new Carousel
            {
                Type = "carousel",
                Contents = bubbles.ToArray()
            };

            Flexmessage[] flexmessages =
            {
                new Flexmessage
                {
                    Type = "flex",
                    altText = "最新公告",
                    contents = carousel
                }
            };

            type = "carousel";

            return JsonConvert.SerializeObject(
                flexmessages,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }

        private string GetTitle(int i)
        {
            switch (i)
            {
                case 1:
                    return "公告事項";
                case 2:
                    return "更新資訊";
                case 3:
                    return "攻略&秘訣";
                case 4:
                    return "自由討論版";
                default:
                    return string.Empty;
            }
        }

        #region 暫時無資料庫使用
        ///// <summary>
        ///// 排隊加入
        ///// </summary>
        ///// <param name="eventData"></param>
        ///// <returns></returns>
        //public string QueueAdd(Event eventData)
        //{
        //    string text = eventData.message.text.Replace("#排隊加入", string.Empty);

        //    using (MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection))
        //    {
        //        string str = "QueueAdd";
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("LineID", eventData.source.userId, dbType: DbType.String);
        //        parameters.Add("num", DBNull.Value, dbType: DbType.Int32, ParameterDirection.Output);
        //        int count = conn.Execute(str, parameters, commandType: CommandType.StoredProcedure);
        //        int num = parameters.Get<int>("num");
        //        if (count == 0)
        //        {
        //            return "已排隊";
        //        }
        //        return "排隊成功，號碼為" + num.ToString();
        //    }
        //}

        ///// <summary>
        ///// 報到
        ///// </summary>
        ///// <param name="eventData"></param>
        ///// <returns></returns>
        //public string AddMember(Event eventData)
        //{
        //    string data = eventData.message.text.Replace("#報到", string.Empty);
        //    string[] arrayData = data.Split("|");
        //    string LineNickName = arrayData[0];
        //    string GameID = arrayData[1];
        //    string GameNickName = arrayData[2];
        //    string GameVersion = arrayData[3];
        //    string GuildName = arrayData[4];

        //    if (GameVersion != "1" && GameVersion != "2" && GameVersion != "3")
        //    {
        //        return "版本錯誤";
        //    }

        //    using MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection);
        //    string str = "AddMember";
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("LineID", eventData.source.userId, dbType: DbType.String);
        //    parameters.Add("LineNickName", LineNickName, dbType: DbType.String);
        //    parameters.Add("GameID", GameID, dbType: DbType.String);
        //    parameters.Add("GameNickName", GameNickName, dbType: DbType.String);
        //    parameters.Add("GameVersion", GameVersion, dbType: DbType.String);
        //    parameters.Add("GuildName", GuildName, dbType: DbType.String);
        //    parameters.Add("Ispresence", DBNull.Value, dbType: DbType.Byte, ParameterDirection.Output);
        //    int count = conn.Execute(str, parameters, commandType: CommandType.StoredProcedure);
        //    bool Ispresence = parameters.Get<byte>("Ispresence").ToString() == "1" ? true : false;
        //    if (count == 0)
        //    {
        //        if (Ispresence)
        //        {
        //            return "會員已存在";
        //        }
        //        else
        //        {
        //            return "新增會員失敗";
        //        }
        //    }
        //    else
        //    {
        //        return "新增會員成功";
        //    }
        //}

        ///// <summary>
        ///// 我找
        ///// </summary>
        ///// <param name="eventData"></param>
        ///// <returns></returns>
        //public string SelectMember(Event eventData)
        //{
        //    string data = eventData.message.text.Replace("#我找", string.Empty);
        //    using (MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection))
        //    {
        //        string str = "SelectMember";
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("KeyWord", data, dbType: DbType.String);
        //        List<Member> memberData = conn.Query<Member>(str, parameters, commandType: CommandType.StoredProcedure).ToList();
        //        if (memberData.Count == 0)
        //        {
        //            return "查無資料";
        //        }
        //        StringBuilder returnStr = new StringBuilder();
        //        returnStr.Append("結果如下 : \n");
        //        foreach (Member item in memberData)
        //        {
        //            returnStr.Append("--------------------------------\n");
        //            returnStr.Append("賴暱稱 : " + item.LineNickName + "\n");
        //            returnStr.Append("遊戲暱稱 : " + item.GameNickName + "\n");
        //            returnStr.Append("遊戲ID : " + item.GameID + "\n");
        //            returnStr.Append("騎士團 : " + item.GuildName + "\n");
        //            returnStr.Append("遊戲版本 : " + item.GameVersion + "\n");
        //        }
        //        return returnStr.ToString();
        //    }

        //}

        ///// <summary>
        ///// 新增騎士團
        ///// </summary>
        ///// <param name="eventData"></param>
        ///// <returns></returns>
        //public string AddGuild(Event eventData)
        //{
        //    string data = eventData.message.text.Replace("#新增騎士團", string.Empty);
        //    using MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection);
        //    string str = "AddGuild";
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("GuildName", data, dbType: DbType.String);
        //    parameters.Add("Ispresence", DBNull.Value, dbType: DbType.Byte, ParameterDirection.Output);
        //    int count = conn.Execute(str, parameters, commandType: CommandType.StoredProcedure);
        //    bool Ispresence = parameters.Get<byte>("Ispresence").ToString() == "1" ? true : false;
        //    if (count == 0)
        //    {
        //        if (Ispresence)
        //        {
        //            return "騎士團已存在";
        //        }
        //        else
        //        {
        //            return "新增騎士團失敗";
        //        }
        //    }
        //    else
        //    {
        //        return "新增騎士團成功";
        //    }
        //}

        ///// <summary>
        ///// 騎士團成員
        ///// </summary>
        ///// <param name="eventData"></param>
        ///// <returns></returns>
        //public string SelectGuildMember(Event eventData)
        //{
        //    string data = eventData.message.text.Replace("#騎士團成員", string.Empty);
        //    using (MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection))
        //    {
        //        string str = "SelectGuildMember";
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("GuildName", data, dbType: DbType.String);
        //        List<Member> memberData = conn.Query<Member>(str, parameters, commandType: CommandType.StoredProcedure).ToList();
        //        if (memberData.Count == 0)
        //        {
        //            return "查無騎士團人員資料";
        //        }
        //        StringBuilder returnStr = new StringBuilder();
        //        returnStr.Append("結果如下 : \n");
        //        returnStr.Append("--------------------------------\n");
        //        foreach (Member item in memberData)
        //        {
        //            returnStr.Append("賴暱稱 : " + item.LineNickName + " ");
        //            returnStr.Append("遊戲暱稱 : " + item.GameNickName + " ");
        //            returnStr.Append("遊戲ID : " + item.GameID + "\n");
        //        }
        //        return returnStr.ToString();
        //    }
        //}
        /// <summary>
        /// 加入關鍵詞
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        //public string AddKeyWord(Event eventData)
        //{
        //    string text = eventData.message.text.Replace("#新增", string.Empty);
        //    string[] arrayData = text.Split("|");
        //    string Key = arrayData[0];
        //    string Value = arrayData[1];
        //    string JsonValue = string.Empty;
        //    JObject jObject = null;
        //    using (MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection))
        //    {
        //        string SelectStr = "SelectKeyWord";
        //        DynamicParameters SelectParameters = new DynamicParameters();
        //        SelectParameters.Add("keyData", Key, dbType: DbType.String);
        //        string JsonData = conn.Query<string>(SelectStr, SelectParameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
        //        if (string.IsNullOrWhiteSpace(JsonData))
        //        {
        //            jObject = new JObject
        //            {
        //                { "Values" , new JArray{ Value } }
        //            };
        //        }
        //        else
        //        {
        //            jObject = JObject.Parse(JsonData);
        //            (jObject["Values"] as JArray).Add(Value);

        //        }

        //        JsonValue = JsonConvert.SerializeObject(jObject);

        //        string Addstr = "AddKeyWord";
        //        DynamicParameters Addparameters = new DynamicParameters();
        //        Addparameters.Add("keyData", Key, dbType: DbType.String);
        //        Addparameters.Add("valuesData", JsonValue, dbType: DbType.String);
        //        conn.Execute(Addstr, Addparameters, commandType: CommandType.StoredProcedure);
        //        return "新增成功";
        //    }
        //}

        ///// <summary>
        ///// 搜尋關鍵詞
        ///// </summary>
        ///// <param name="eventData"></param>
        ///// <returns></returns>
        //public string SelectKeyWord(Event eventData)
        //{
        //    string text = eventData.message.text.Replace("#", string.Empty);
        //    JObject jObject = null;
        //    using (MySqlConnection conn = new MySqlConnection(_settings.Value.DB.MySQL.ConnectionStrings.SevenDeadlySinsConnection))
        //    {
        //        string SelectStr = "SelectKeyWord";
        //        DynamicParameters SelectParameters = new DynamicParameters();
        //        SelectParameters.Add("keyData", text, dbType: DbType.String);
        //        string JsonData = conn.Query<string>(SelectStr, SelectParameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
        //        if (string.IsNullOrWhiteSpace(JsonData))
        //        {
        //            return "查無關鍵詞";
        //        }
        //        else
        //        {
        //            jObject = JObject.Parse(JsonData);
        //        }
        //        Random random = new Random();
        //        int i = random.Next(0, (jObject["Values"] as JArray).Count);
        //        return (jObject["Values"] as JArray)[i].ToString();
        //    }
        //}
        #endregion 暫時無資料庫使用

    }
}
