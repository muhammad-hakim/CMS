using System;

namespace CMS.Models
{
    public class Relation
    {
        public string guid { get; set; }
        public string fromSite { get; set; }
        public string fromContent { get; set; }

        public string toSite { get; set; }
        public string toContent { get; set; }

        public DateTime created { get; set; }
        public string createdBy { get; set; }      

        public DateTime updated { get; set; }

    }
}
