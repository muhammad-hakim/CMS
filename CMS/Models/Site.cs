using Newtonsoft.Json;
using System.Collections.Generic;
using CMS.Types;

namespace CMS.Models
{
    public class Site
    {
        public string code { get; set; }

        public string type { get; set; }

        public List<BilingualString> categories { get; set; }
        public BilingualString name { get; set; }
        public BilingualString description { get; set; }

        public BilingualString more { get; set; }

        public List<string> access_rate { get; set; }
        public List<string> access_comment { get; set; }
        public List<string> access_readers { get; set; }
        public List<string> access_editors { get; set; }
        public List<string> access_publishers { get; set; }

        public bool canRate
        {
            get
            {
                return User.isMember(access_rate);
            }
        }
        public bool canComment
        {
            get
            {
                return User.isMember(access_comment);
            }
        }

        public bool canReadReader
        {
            get
            {
                return User.isMember(access_readers) || canEdit || canPublish;
            }
        }

        public bool canRead
        {
            get
            {
                return User.isMember(access_readers) || canEdit || canPublish || 1==1;
            }
        }

        public bool canEdit
        {
            get
            {
                return User.isMember(access_editors) || User.isMember(access_publishers);
            }
        }

        internal bool canPublish
        {
            get
            {
                return User.isMember(access_publishers);
            }
        }

        internal bool canReject
        {
            get
            {
                return User.isMember(access_publishers);
            }
        }

        internal bool canDelete
        {
            get
            {
                return User.isMember(access_publishers) || User.isMember(access_editors);
            }
        }

        internal bool canHide
        {
            get
            {
                return User.isMember(access_publishers);
            }
        }

        [JsonIgnore]
        internal string folderPath
        {
            get
            {
                var _path = Config.WWWPath + "forms/" + code + "/";

                return _path;
            }
        }

        [JsonIgnore]
        internal string filePath
        {
            get
            {
                return folderPath + "site.json";
            }
        }

        public static Site GetSite(string code)
        {
            Site site = new Site() { code = code };

            if (System.IO.File.Exists(site.filePath))
            {
                string text = System.IO.File.ReadAllText(site.filePath);

                site = JsonConvert.DeserializeObject<Site>(text);
            }

            return site;
        }
        // Added by Kalam on 30/01/2020, for logo link
        public string logoLink { get; set; }

        //void Save()
        //{
        //    var output = Newtonsoft.Json.JsonConvert.SerializeObject(this);

        //    if (!System.IO.Directory.Exists(folderPath)) System.IO.Directory.CreateDirectory(folderPath);

        //    System.IO.File.WriteAllText(filePath, output);
        //}
    }
}
