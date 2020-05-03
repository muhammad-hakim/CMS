using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMS.Models;
using System.Linq;

namespace CMS.Controllers
{
    /// <summary>
    /// COMMENT CONTROLLER
    /// </summary>
    [ApiController]
    [Route("cms3/services/contents/{siteCode}/{contentCode}/{contentVersion}/[controller]")] 
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<ContentsController> _logger;

        public CommentsController(ILogger<ContentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetComments(string siteCode, string contentCode, string contentVersion)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);
          
            if (content.site.canComment)
            {
                return Ok(content.commentList.comments.Where<Comment>(c => !string.IsNullOrEmpty(c.text)));
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }



        [HttpPost]
        public IActionResult Post(string siteCode, string contentCode, string contentVersion, Comment comment)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (content.site.canComment)
            {
                content.AddComment(comment);

                return Ok(new { success = true });
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }

        [HttpPost("{guid}/publish")]
        public IActionResult Publish(string siteCode, string contentCode, string contentVersion, string guid)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (!content.site.canPublish) Unauthorized(new { success = false });

            content.PublishComment(guid);

            return Ok(new { success = true });
        }
    }
}
