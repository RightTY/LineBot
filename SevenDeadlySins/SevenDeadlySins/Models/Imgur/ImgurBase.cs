using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;

namespace SevenDeadlySins.Models.Imgur
{
    public class ImgurBase: ImgurBaseModel
    {
        #region Imgur
        /// <summary>
        /// 
        /// </summary>
        public ApiClient imgurClient { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ImageEndpoint imageEndpoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IImage image { get; set; }
        #endregion Imgur

        private readonly HttpClient HttpClient = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CLIENT_ID"></param>
        public void SetCLIENT_ID(string CLIENT_ID)
        {
            this.CLIENT_ID = CLIENT_ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CLIENT_SECRET"></param>
        public void SetCLIENT_SECRET(string CLIENT_SECRET)
        {
            this.CLIENT_SECRET = CLIENT_SECRET;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OAuth2Token"></param>
        public void SetOAuth2Token(string RefreshToken)
        {
            OAuth2Endpoint endpoint = new(imgurClient, HttpClient);
            IOAuth2Token OAuth2Token = endpoint.GetTokenAsync(RefreshToken).GetAwaiter().GetResult();
            imgurClient.SetOAuth2Token(OAuth2Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="albumId"></param>
        public void SetAlbumId(string albumId)
        {
            this.albumId = albumId;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetImgurClient()
        {
            imgurClient = new ApiClient(CLIENT_ID, CLIENT_SECRET);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetImageEndpoint()
        {
            imageEndpoint = new ImageEndpoint(imgurClient, HttpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public IImage UploadImageAsync(Stream stream)
        {
            return imageEndpoint.UploadImageAsync(stream, albumId).GetAwaiter().GetResult();
        }
    }
}
