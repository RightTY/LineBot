using isRock.LineBot;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SevenDeadlySins.BLL;
using SevenDeadlySins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SevenDeadlySins.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IOptions<Setting> _settings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public MasterController(IOptions<Setting> settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// POST: SevenDeadlySins/LineBot/Master
        /// </summary>
        /// <param name="postData"></param>
        [HttpPost]
        public IActionResult Post(ReceivedMessage receievedMessage)
        {
            try
            {
                foreach (Event @event in receievedMessage.events)
                {
                    string type = @event.message.type;
                    LinebotReply linebotReply = new LinebotReply(_settings);
                    switch (type)
                    {
                        case "text":
                            Regex regex = new Regex(@"^#");
                            if (regex.IsMatch(@event.message.text))
                            {
                                linebotReply.Text(@event);
                            }
                            break;

                        case "sticker":
                            //linebotReply.Sticker(@event);
                            break;

                        case "image":
                            linebotReply.Image(@event);
                            break;

                        default:
                            break;
                    }
                }
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }

}
