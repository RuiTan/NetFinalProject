using NetFinalProject.Models;
using System.Collections.Generic;
using System.Web.Http;
using NetFinalProject.Service;

namespace NetFinalProject.Controllers
{
    public class CommentController : ApiController
    {

        [HttpGet]
        [Route("api/comment/tocken")]
        public string tocken()
        {
            return CommentService.tocken();
        }

        [HttpGet]
        [Route("api/comment/upload")]
        public Dictionary<string, object> upload(int movieId, string title, string intro, string cover, string content)
        {
            return CommentService.upload(movieId, title, intro, cover, content);
        }

        [HttpGet]
        [Route("api/comment/list")]
        public LinkedList<Dictionary<string, object>> list(int movieId)
        {
            return CommentService.list(movieId);
        }

        [HttpGet]
        [Route("api/comment/listByPage")]
        public LinkedList<Dictionary<string, object>> listByPage(int page)
        {
            return CommentService.listByPage(page);
        }

        [HttpGet]
        [Route("api/comment/listByUser")]
        public LinkedList<Dictionary<string, object>> listByUser(int userId)
        {
            return CommentService.listByUser(userId);
        }
    }
}
 