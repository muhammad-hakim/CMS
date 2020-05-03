using System;

namespace CMS.Models
{
    public class Comment
    {
        public string guid { get; set; }
        public string text { get; set; }
        public DateTime created { get; set; }
        public string createdBy { get; set; }
        public string author { get; set; }
        public string authorEmail { get; set; }
        public DateTime published { get; set; }
        public string publishedBy { get; set; }
    }
}
