using SevenDeadlySins.Models.Line.LineBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line
{
    public class LineSettingModel
    {
        /// <summary>
        /// 頻道ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// LineLoginProjectSetting-Authorize
        /// </summary>
        public Authorize Authorize { get; set; }
        /// <summary>
        /// LineLoginProjectSetting-Token
        /// </summary>
        public Token Token { get; set; }
        /// <summary>
        /// LineLoginProjectSetting-Profile
        /// </summary>
        public Profile Profile { get; set; }
        /// <summary>
        /// 頻道金鑰
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
