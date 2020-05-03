using System;

namespace CMS.Models
{
    public class Version
    {
        public string number { get; set; }
        public string changes { get; set; }
        public DateTime modified { get; set; }
        public string modifiedBy { get; set; }        
        public DateTime published { get; set; }
        public string publishedBy { get; set; }
    }
}
