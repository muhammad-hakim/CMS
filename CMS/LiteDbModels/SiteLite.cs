using CMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.LiteDbModels
{
    public class SiteLite
    {
        public SiteLite(Site site)
        {
            Code = site.code;
            Type = site.type;
            NameAr = site.name.ar;
            NameEn = site.name.en;
            DescriptionAr = site.description.ar;
            DescriptionEn = site.description.en;
            MoreAr = site.more.ar;
            MoreEn = site.more.en;
            ImageUrl = site.logoLink;
        }

        public SiteLite() { }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string NameAr{ get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string MoreAr { get; set; }
        public string MoreEn { get; set; }
        public string ImageUrl { get; set; }
    }
}
