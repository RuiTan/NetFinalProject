using NetFinalProject.Models;
using NetFinalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NetFinalProject.Controllers
{
    [Route("api/chat")]
    public class ChatController : ChatBaseController<ChatHub>
    {
        [HttpGet]
        [Route("api/chat/send")]
        public async Task send(int userId, int movieId, string content)
        {
            await Clients.All.SendMessage(ClassToDictionary.classToDictionary(ChatService.save(userId, movieId, content)));
        }

        [HttpGet]
        [Route("api/chat/list")]
        public LinkedList<Dictionary<string, object>> list(int movieId)
        {
            return ChatService.list(movieId);
        }
    }
}
