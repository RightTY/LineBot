using Microsoft.Extensions.Options;
using SevenDeadlySins.Models;
using SevenDeadlySins.Models.Imgur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.BLL
{
    public class ImgurBLL : ImgurBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public ImgurBLL(IOptions<Setting> settings)
        {
            SetCLIENT_ID(settings.Value.ImgurSetting.CLIENT_ID);
            SetCLIENT_SECRET(settings.Value.ImgurSetting.CLIENT_SECRET);
            SetImgurClient();
            SetImageEndpoint();
            SetOAuth2Token(settings.Value.ImgurSetting.RefreshToken);
            SetAlbumId(settings.Value.ImgurSetting.albumId);
        }
    }
}
