using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class duration
    {
    }

    public class steps
    {
    }

    public class requirements
    {
    }

    public class Attachments
    {
    }

    public class ar
    {
        public string name { get; set; }
        public string serviceTime { get; set; }
        public string serviceCost { get; set; }
        public string serviceDescription { get; set; }
        public string BeneficiaryCategory { get; set; }
        public string serviceUse { get; set; }
        public string BasicRequirment { get; set; }
    }

    public class en
    {
        public string name { get; set; }
        public string serviceTime { get; set; }
        public string serviceCost { get; set; }
        public string serviceDescription { get; set; }
        public string BeneficiaryCategory { get; set; }
        public string serviceUse { get; set; }
        public string BasicRequirment { get; set; }
    }

    public class requirements1
    {
        public ar ar { get; set; }
        public en en { get; set; }
        public string attachmentKey { get; set; }
    }

   

    public class requirements2
    {
        public ar ar { get; set; }
        public en en { get; set; }
        public string attachmentKey { get; set; }
    }

  

   

    public class requirements3
    {
        public ar ar { get; set; }
        public en en { get; set; }
        public string attachmentKey { get; set; }
    }

    public class List1
    {
    }

    public class List2
    {
    }

    public class Requirements4
    {
        public List1 list1 { get; set; }
        public List2 list2 { get; set; }
        public string attachmentKey { get; set; }
    }

    

    public class Requirements5
    {
        public List1 list1 { get; set; }
        public List2 list2 { get; set; }
        public string attachmentKey { get; set; }
    }

    public class url
    {
    }

    public class RootObject
    {
        public duration duration { get; set; }
        public steps steps { get; set; }
        public requirements requirements { get; set; }
        public Attachments attachments { get; set; }
        public requirements1 requirements1 { get; set; }
        public requirements2 requirements2 { get; set; }
        public requirements3 requirements3 { get; set; }
        public Requirements4 requirements4 { get; set; }
        public Requirements5 requirements5 { get; set; }
        public url url { get; set; }
        public string serviceUrl_ar { get; set; }
        public string serviceUrl_en { get; set; }
        public string guidelineURL_ar { get; set; }
        public string guidelineURL_en { get; set; }
    }
}
