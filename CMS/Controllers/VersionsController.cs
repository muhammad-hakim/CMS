using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [ApiController]
    [Route("cms3/services/contents/{siteCode}/{contentCode}/{contentVersion}/[controller]")]
    public class VersionsController : ControllerBase
    {
        private readonly ILogger<ContentsController> _logger;

        public VersionsController(ILogger<ContentsController> logger)
        {
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(string siteCode, string contentCode, string contentVersion)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (content.CanViewHistory)
            {
                return Ok(content.versions);
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult Post(string siteCode, string contentCode, string contentVersion)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            System.Collections.Hashtable _links = new System.Collections.Hashtable();

            if (content.CanCreateVersion)
            {
                content.version = (float.Parse(content.version) + 0.1).ToString("0.0");
                content.Save();

                _links.Add("Edit", $"{siteCode}/{contentCode}/{content.version}");

                Response.Headers.Add("_links", Newtonsoft.Json.JsonConvert.SerializeObject(_links));

                return Ok(new { sucess = true, version = content.version });
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }
    }
}
