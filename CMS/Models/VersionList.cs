using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class VersionList
    {
        public string publishedVersion { get; set; }
        public string latestVersion { get; set; }
        public string latestVersionStatus { get; set; }

        public List<Version> versions { get; set; }

        internal string contentPath { get; set; }
      
        internal string filePath
        {
            get
            {
                return contentPath + "\\" + "_versions.json";
            }
        }

        public static VersionList GetList(Content content)
        {
            VersionList list = new VersionList() { contentPath = content.folderPath };

            if (System.IO.File.Exists(list.filePath))
            {
                string text = System.IO.File.ReadAllText(list.filePath);

                list = JsonConvert.DeserializeObject<VersionList>(text);

                list.contentPath = content.folderPath;
            }

            list.publishedVersion = string.IsNullOrEmpty(list.publishedVersion) ? "0" : list.publishedVersion;
            list.versions = list.versions == null ? new List<Version>() : list.versions;

            return list;
        }

        public void Update(string version)
        {
            latestVersion = version;
            latestVersionStatus = "Draft";
            
            var contentVersion = versions.Find(v => v.number == version);

            if(contentVersion == null)
            {
                contentVersion = new Version() { number = version, modifiedBy = User.username, modified = DateTime.Now };

                versions.Add(contentVersion);
            }
            else
            {
                contentVersion.modified = DateTime.Now;
                contentVersion.modifiedBy = User.username;
            }

            Save();
        }

        public void Publish(Content content, string changes)
        {
            latestVersion = content.version;
            publishedVersion = content.version;
            latestVersionStatus = "Published";

            var contentVersion = versions.Find(v => v.number == content.version);

            contentVersion.published = DateTime.Now;
            contentVersion.publishedBy = User.username;
            contentVersion.changes = changes;

            Save();
        }

        public void RequestReview(Content content)
        {
            if (content != null)
            {
                latestVersion = content.version;
                latestVersionStatus = "Review";

                Save();
            }
           
        }

        void Save()
        {
            var output = JsonConvert.SerializeObject(this);

            System.IO.File.WriteAllText(filePath, output);
        }
    }
}
