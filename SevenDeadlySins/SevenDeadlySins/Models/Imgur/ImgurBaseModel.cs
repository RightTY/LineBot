using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Imgur
{
    public class ImgurBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string CLIENT_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CLIENT_SECRET { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string albumId { get; set; }
}
}
