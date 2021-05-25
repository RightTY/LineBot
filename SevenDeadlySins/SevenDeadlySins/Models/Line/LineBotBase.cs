using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line
{
    /// <summary>
    /// LineBotBase
    /// </summary>
    public class LineBotBase
    {
        private LineWebHookControllerBase bot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChannelAccessToken"></param>
        public void SetBot(string ChannelAccessToken)
        {
            bot = new LineWebHookControllerBase
            {
                ChannelAccessToken = ChannelAccessToken
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="replyMessage"></param>
        /// <param name="IsJson"></param>
        /// <returns></returns>
        public virtual string Reply(Event eventData, string replyMessage, string type)
        {
            if (type == "carousel")
            {
                return bot.ReplyMessageWithJSON(
                    eventData.replyToken,
                    replyMessage
                );
            }
            return bot.ReplyMessage(eventData.replyToken, replyMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="replyMessages"></param>
        /// <returns></returns>
        public virtual string ReplyMessages(Event eventData, List<MessageBase> replyMessages)
        {

            return bot.ReplyMessage(eventData.replyToken, replyMessages);
        }
        /// <summary>
        /// 取得圖檔資料
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public virtual byte[] GetImage(Event eventData)
        {
            return bot.GetUserUploadedContent(eventData.message.id);
        }
    }
}
