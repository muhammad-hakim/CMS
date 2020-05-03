using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("cms3/services/contents/{siteCode}/{contentCode}/{contentVersion}/rating")]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<ContentsController> _logger;

        public RatingsController(ILogger<ContentsController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]        
        public IActionResult Post(string siteCode, string contentCode, string contentVersion, Rating rating)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (content.site.canRate)
            {
                content.AddRating(rating);

                return Ok(new { success = true });
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }
    }
}
