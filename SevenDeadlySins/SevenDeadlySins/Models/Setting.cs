using SevenDeadlySins.Models.Database;
using SevenDeadlySins.Models.Imgur;
using SevenDeadlySins.Models.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models
{
    public class Setting
    {
        /// <summary>
        /// DB設定檔
        /// </summary>
        public DatabaseSettingModel DatabaseSetting { get; set; }
        /// <summary>
        /// Line設定檔
        /// </summary>Model
        public LineSettingModel LineSetting { get; set; }
        /// <summary>
        /// Imgur設定檔
        /// </summary>
        public ImgurBaseModel ImgurSetting { get; set; }
    }
}
