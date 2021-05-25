using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBase
{
    /// <summary>
    /// Authorize
    /// </summary>
    public class Authorize
    {
        /// <summary>
        /// authorize URL
        /// </summary>
        public Uri AuthorizeUri { get; set; }
        /// <summary>
        /// 請求項目
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 回調URL
        /// </summary>
        public Uri RedirectUri { get; set; }
        /// <summary>
        /// 這告訴LINE平台返回授權碼
        /// </summary>
        public string ResponseType { get; set; }
        /// <summary>
        /// 用於防止跨站點請求偽造的唯一字母數字字符串。
        /// <para></para>
        /// 該值應由您的應用程序隨機生成。不能是URL編碼的字符串。
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 用於防止重放攻擊的字符串。此值以ID令牌返回。(非必要)
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// 即使用戶已授予所有請求的權限，也用於強制顯示同意屏幕。(非必要)
        /// </summary>
        public string Prompt { get; set; }
        /// <summary>
        /// 自上次驗證用戶以來的允許經過時間（以秒為單位）。(非必要)
        /// <para></para>
        /// 對應於OpenID Connect Core 1.0max_age的“身份驗證請求”部分中定義的參數。
        /// </summary>
        public int Max_age { get; set; }
        /// <summary>
        /// LINE登錄屏幕的顯示語言。按首選項順序指定為一個或多個RFC 5646（BCP 47）語言標籤，
        /// <para></para>
        /// 以空格分隔。對應於OpenID Connect Core 1.0ui_locales的“身份驗證請求”部分中定義的參數。(非必要)
        /// </summary>
        public string UiLocales { get; set; }
        /// <summary>
        /// 顯示在登錄期間將LINE官方帳戶添加為朋友的選項。將值設置為normal或aggressive。(非必要)
        /// <para></para>
        /// 有關更多信息，請參閱將LINE官方帳戶與LINE登錄頻道相關聯。
        /// </summary>
        public string BotPrompt { get; set; }
    }
}
