using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CMS.Models;

namespace myWebApi.Controllers
{
    [ApiController]
    [Route("cms3/services/contents/{siteCode}/{contentCode}/{contentVersion}/[controller]")]
    public class RelationsController : ControllerBase
    {
        private readonly ILogger<RelationsController> _logger;

        public RelationsController(ILogger<RelationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(string siteCode, string contentCode, string contentVersion, List<Relation> relations)
        {
            var content = CMS.Models.Content.GetContent(siteCode, contentCode, contentVersion);

            if (content.CanSave)
            {
                RelationList.AddContentRelation(siteCode, contentCode, relations);

                return Ok(new { sucess = true, version = content.version });
            }
            else
            {
                return Unauthorized(new { sucess = false, version = content.version });
            }
        }
    }
}
