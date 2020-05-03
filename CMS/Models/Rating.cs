using System;

namespace CMS.Models
{
    public class Rating
    {
        public string guid { get; set; }
        public int value { get; set; }
        public DateTime created { get; set; }
        public string createdBy { get; set; }
    }
}
