using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Line.LineBase
{
    /// <summary>
    /// Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 回調URL
        /// </summary>
        public Uri RedirectUri { get; set; }
        /// <summary>
        /// authorize URL
        /// </summary>
        public Uri TokenUri { get; set; }
        /// <summary>
        /// 指定授予類型。
        /// </summary>
        public string GrantType { get; set; }
    }
}
