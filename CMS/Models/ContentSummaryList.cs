using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class ContentSummaryList
    {
        public string siteCode { get; set; }

        public List<ContentSummary> contents { get; set; }


        [JsonIgnore]
        internal string folderPath
        {
            get
            {
                var _path = Config.ContentPath + siteCode + "/";

                return _path;
            }
        }

        [JsonIgnore]
        internal string filePath
        {
            get
            {
                return folderPath + "_contents.json";
            }
        }

        public static ContentSummaryList GetList(string code, bool PublishedOnly)
        {           
            ContentSummaryList contents = new ContentSummaryList() { siteCode = code };

            if (System.IO.File.Exists(contents.filePath))
            {
                string text = System.IO.File.ReadAllText(contents.filePath);

                contents = JsonConvert.DeserializeObject<ContentSummaryList>(text);
            }

            contents.contents = contents.contents == null ? new List<ContentSummary>() : contents.contents;

            if(PublishedOnly )
            {
                contents.contents = contents.contents.FindAll(c => !string.IsNullOrEmpty(c.publishedVersion));

                contents.contents.ForEach(c => { c.publishedBy = null; c.latestVersionLastModifiedBy = null; c.latestVersionStatus = null; });
            }

            return contents;
        }

        public void UpdateContent(Content content)
        {
           //   GetList(content.siteCode,content.CanPublish);
            var contentSummary = contents.Find(a => a.code == content.code);

            if (contentSummary == null)
            {
                contentSummary = new ContentSummary()
                {
                    hscode=content.HSCODE,
                    code = content.code,
                    subject = content.subject,
                    category = content.category,
                    tags = content.tags,                    
                    latestVersion = content.version,
                    description = content.description,
                    latestVersionStatus = "Draft",
                    latestVersionLastModifiedBy = User.username,
                    latestVersionLastModifiedDate = DateTime.Now,
                    rejectionNotes = content.rejectionNotes,
                    IsHide = content.IsHide,
                    IsDelete = content.IsDelete,
                    IsReject = content.IsReject,
                };

                contents.Add(contentSummary);
            }
            else
            {
                contentSummary.latestVersion = content.version;                
                contentSummary.latestVersionStatus = "Draft";
                contentSummary.latestVersionLastModifiedBy = User.username;
                contentSummary.latestVersionLastModifiedDate = DateTime.Now;
                contentSummary.rejectionNotes = content.rejectionNotes;
                contentSummary.IsHide = content.IsHide;
                contentSummary.IsDelete = content.IsDelete;
                contentSummary.IsReject = content.IsReject;
            }

            Save();
        }

        public void RequestReview(Content content)
        {
            var contentSummary = contents.Find(a => a.code == content.code);
            //if (contents.Count<2 && contentSummary == null)
            //{
            //    contentSummary = contents[0];
            //}
            if (contentSummary != null)
            {
                contentSummary.latestVersion = content.version;
                contentSummary.latestVersionStatus = "Review";

                Save();
            }
          
        }

        public void Publish(Content content)
        {
            var contentSummary = contents.Find(a => a.code == content.code);

            if (contentSummary == null)
            {
                contentSummary = new ContentSummary()
                {
                    code = content.code,
                    subject = content.subject,
                    category = content.category,
                    tags = content.tags,
                    latestVersion = content.version,
                    description = content.description,
                    latestVersionStatus = "Published",
                    publishedBy = User.username,
                    publishedVersion = content.version,
                    publishedDate = DateTime.Now
                };

                contents.Add(contentSummary);
            }
            else
            {
                contentSummary.latestVersion = content.version;
                contentSummary.subject = content.subject;
                contentSummary.category = content.category;
                contentSummary.description = content.description;
                contentSummary.tags = content.tags;
                contentSummary.latestVersionStatus = "Published";
                contentSummary.publishedBy = CMS.User.username;
                contentSummary.publishedVersion = content.version;
                contentSummary.publishedDate = DateTime.Now;
            }

            Save();
        }

        void Save()
        {
            var output = JsonConvert.SerializeObject(this);

            if (!System.IO.Directory.Exists(folderPath)) System.IO.Directory.CreateDirectory(folderPath);

            System.IO.File.WriteAllText(filePath, output);
        }
    }
}
