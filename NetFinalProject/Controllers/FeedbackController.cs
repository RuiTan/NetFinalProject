using NetFinalProject.Models;
using NetFinalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetFinalProject.Controllers
{
    public class FeedbackController : ApiController
    {

        [HttpGet]
        [Route("api/feedback/list")]
        public List<Dictionary<string, object>> list(int commentId)
        {
            return FeedbackService.list(commentId);
        }

        [HttpGet]
        [Route("api/feedback/reply")]
        public Dictionary<string, object> reply(int commentId, int atId, string content, int replyId)
        {
            return FeedbackService.reply(commentId, atId, content, replyId);
        }

        [HttpGet]
        [Route("api/feedback/like")]
        public Dictionary<string, object> like(int commentId)
        {
            return FeedbackService.like(commentId);
        }

        [HttpGet]
        [Route("api/feedback/getLike")]
        public Dictionary<string, object> getLike(int commentId)
        {
            return FeedbackService.getLike(commentId);
        }

    }
}
