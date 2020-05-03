using System;
using System.Collections.Generic;
using CMS.Types;

namespace CMS.Models
{
    public class ContentSummary
    {

        public List<string> hscode { get; set; }
        public string code { get; set; }
        public BilingualString subject { get; set; }

        public string category { get; set; }
        public BilingualStringList tags { get; set; }

        public BilingualString description { get; set; }

        public string rejectionNotes { get; set; }
        public bool IsHide { get; set; }
        public bool IsReject { get; set; } = false;
        public bool IsDelete { get; set; } = false;


        public string publishedVersion { get; set; }
        public DateTime publishedDate { get; set; }

        public string publishedBy { get; set; }
        public string latestVersion { get; set; }
        public string latestVersionStatus { get; set; }
        public DateTime latestVersionLastModifiedDate { get; set; }
        public string latestVersionLastModifiedBy { get; set; }
    }
}
