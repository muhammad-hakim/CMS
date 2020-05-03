using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

namespace CMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
   // [RequireHttps]
    [AllowAnonymous]
    [ApiController]
    [Route("cms3/services/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly ILogger<ContentsController> _logger;


        
        public SitesController(ILogger<ContentsController> logger)
        {
            _logger = logger;
          
        }

       //  [RequireHttps]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetList()
        {
            List<dynamic> sites = new List<dynamic>();

            foreach (string directory in Directory.GetDirectories(Config.WWWPath + "forms"))
            {
                var directoryInfo = new DirectoryInfo(directory);

                Site site = Site.GetSite(directoryInfo.Name);

                var contents = ContentSummaryList.GetList(site.code,(!site.canEdit|| HttpContext.Request.Cookies["user_Lognstatus"] == null));

                // Added by Kalam on 26/01/2020, for load the site based on selected category
                // changed by Imran to check if any category is selected or no.
                if ((CMS.Models.clsCategory.selectedCategory != null) &&
                     (site.categories.Exists(x => x.en.ToUpper().Equals(CMS.Models.clsCategory.selectedCategory.ToUpper())) ||
                        site.categories.Exists(x => x.ar.ToUpper().Equals(CMS.Models.clsCategory.selectedCategory.ToUpper())))
                    )
                {
                    //if (site.canRead)
                    //{
                    // Modified by Kalam on 30/01/2020, for logo link
                    sites.Add(new { site.code, site.name, site.description, site.more, canCreate = site.canEdit, site.categories, contents.contents, site.logoLink });
                    //}
                }
            }

            return Ok(sites);
        }

        [HttpGet("{siteCode}")]
        public IActionResult GetSite(string siteCode)
        {
            Site site = Site.GetSite(siteCode);

            if (site.canRead)
            {
                return Ok(new { site.code, site.name, site.description, site.canEdit, site.categories, site.canRead });
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }

        [HttpGet("{siteCode}/contents")]
        public IActionResult GetSummaryList(string siteCode)
        {
            Site site = Site.GetSite(siteCode);

            if (site.canRead)
            {
                return Ok(ContentSummaryList.GetList(siteCode, (!site.canEdit || HttpContext.Request.Cookies["user_Lognstatus"] == null)));
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }
    }
}
