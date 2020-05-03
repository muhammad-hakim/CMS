using CMS.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace CMS.Models
{
    public class Content
    {


        /// public string HSCODE { get; set; }
        /// 
        public List<string> HSCODE { get; set; }
        public bool isNew { get; set; }

        public string siteCode { get; set; }
        //   [RegularExpression("^[a-zA-Z0-9\\-s@,=%$#&_\u0621-\u064A\u0660-\u0669]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //  [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
        ///   [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_{}:]*$", ErrorMessage = "Use Characters only(code)")]
        [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]

        public string code { get; set; }
        public string version { get; set; }
        public string category { get; set; }
        ///  [RegularExpression("^[a-zA-Z0-9_@./\\-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //    [RegularExpression("^(?:[a-zA-Z0-9\\-s@,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){0,30}$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //    [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
        ///  [RegularExpression("^(?:[a-zA-Z0-9\\-s@,=%$#&_\u0621-\u064A\u0660-\u0669]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
      ///  [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_{}:]*$", ErrorMessage = "Use Characters only(code)")]
     
        public BilingualString subject { get; set; }

        ///  [RegularExpression("^(?:[a-zA-Z0-9\\-s@,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){0,30}$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        /// [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
        /// [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_{}:]*$", ErrorMessage = "Use Characters only(code)")]
    
        public BilingualString description { get; set; }
        //   [RegularExpression("^(?:[a-zA-Z0-9\\-s@,=%$#&_\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\uFB50-\uFDCF\uFDF0-\uFDFF\uFE70-\uFEFF]|(?:\uD802[\uDE60-\uDE9F]|\uD83B[\uDE00-\uDEFF])){0,30}$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        ///  [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
    ///    [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_{}:]*$", ErrorMessage = "Use Characters only(code)")]

        public BilingualStringList tags { get; set; }
        public int ratingsValue { get; set; }
        public int countPublishedComments { get; set; }
        public int countVotes { get; set; }

        public string rejectionNotes { get; set; }


        ///  [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        ///    [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
        ///  [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_{}:]*$", ErrorMessage = "Use Characters only(code)")]
        [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
        public string body { get; set; }

        public bool publishedOnly { get; set; } = true;
        //[JsonIgnore]
        //public bool rejectOnly { get; set; } = false;
        //[JsonIgnore]
        //public bool hideOnly { get; set; } = false;
        //[JsonIgnore]
        //public bool deleteOnly { get; set; } = false;

        public bool IsReject { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public bool IsHide { get; set; } = false;



        public List<Attachment> attachments { get; set; }


        Site _site;
        internal Site site
        {
            get
            {
                _site = _site == null ? Site.GetSite(siteCode) : _site;

                return _site;
            }
        }

        VersionList _versions;
        internal VersionList versions
        {
            get
            {
                _versions = _versions == null ? VersionList.GetList(this) : _versions;

                return _versions;
            }
        }

        ContentSummaryList _summaryList;
        internal ContentSummaryList summaryList
        {
            get
            {
                //_summaryList = _summaryList == null ? ContentSummaryList.GetList(this.siteCode, true) : _summaryList;
                _summaryList = _summaryList == null ? ContentSummaryList.GetList(this.siteCode, false) : _summaryList;
                return _summaryList;
            }
        }

        RatingList _ratingList;
        internal RatingList ratingList
        {
            get
            {
                _ratingList = _ratingList == null ? RatingList.GetList(this) : _ratingList;

                return _ratingList;
            }
        }

        CommentList _commentList;
        internal CommentList commentList
        {
            get
            {
                _commentList = _commentList == null ? CommentList.GetList(this) : _commentList;

                return _commentList;
            }
        }

        internal string folderPath
        {
            get
            {
                var _path = Config.ContentPath + siteCode + "/" + code.Substring(code.Length - 2) + "/" + code + "/";

                return _path;
            }
        }

        internal string filePath
        {
            get
            {
                return folderPath + code + "_" + version.Replace(".", "-") + ".json";
            }
        }

        internal string attachmentsPath
        {
            get
            {
                return folderPath + "attachments/";
            }
        }

        public bool CanComment
        {
            get
            {
                return site.canComment;
            }

        }

        public bool CanRate
        {
            get
             {
                return site.canRate;
            }

        }

        public bool CanViewHistory
        {
            get
            {
                return site.canEdit;
            }

        }

        internal bool CanRead
        {
            get
            {
                if (site.canEdit) return true;

                return site.canRead;// && version == versions.publishedVersion;
            }

        }

        internal bool CanSave
        {
            get
            {
                if (!site.canEdit) return false;

                if (version != "undefined")
                {
                    if (float.Parse(version) <= float.Parse(versions.publishedVersion))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        internal bool CanCreateVersion
        {
            get
            {
                if (!site.canEdit) return false;

                if (float.Parse(version) != float.Parse(versions.publishedVersion) || versions.publishedVersion != versions.latestVersion)
                {
                    return false;
                }
                return true;
            }

        }

        internal bool CanPublish
        {
            get
            {
                if (!site.canPublish) return false;
                return CanSave;
            }
        }

        public bool CanReadReader
        {
            get
            {
                if (site.canReadReader) return true;

                else return false;
            }

        }

        public bool CanReject
        {
            get
            {
                if (site.canReject)
                    return true;
                else return false;
                //return CanSave;
            }
        }

        public bool CanDelete
        {
            get
            {
                if (site.canDelete) return true;
                else return false;
                //return CanSave;
            }
        }

        public bool CanHide
        {
            get
            {
                if (site.canHide)
                    return  true;
                else return false;
                //return CanSave;
            }
        }

        internal bool CanRequestReview
        {
            get
            {
                if (!site.canEdit) return false;

                if (float.Parse(version) != float.Parse(versions.latestVersion) || versions.latestVersionStatus != "Draft")
                {
                    return false;
                }

                return CanSave;
            }

        }

        public List<Relation> GetRelations()
        {
            return RelationList.GetRelations(siteCode, code);
        }

        public IEnumerable<Comment> GetComments()
        {
            return commentList.comments.Where(c => !string.IsNullOrEmpty(c.text));
        }
        public void Save(bool UpdateStatus = true)
        {
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            if (!System.IO.Directory.Exists(folderPath)) System.IO.Directory.CreateDirectory(folderPath);

            
            System.IO.File.WriteAllText(filePath, output);

            if (UpdateStatus)
            {
                //update history
                versions.Update(version);
                //update site
                publishedOnly = false;
                summaryList.UpdateContent(this);
            }
        }

        public static Content GetContent(string siteCode, string contentCode, string contentVersion)
        {
            //var content = new Content() { siteCode = siteCode, code = contentCode, version = contentVersion, CanReject = true , rejectOnly = false };


            var content = new Content() { siteCode = siteCode, code = contentCode, version = contentVersion};
            if (System.IO.File.Exists(content.filePath))
            {
                string text = System.IO.File.ReadAllText(content.filePath);

                content = Newtonsoft.Json.JsonConvert.DeserializeObject<Content>(text);

                if (string.IsNullOrEmpty(content.body)) content.body = "{}";
            }

            //content.rejectOnly = false;
            //content.hideOnly = false;
            //content.deleteOnly = false;
            //if (content.CanReject)
            //{
            //    content.rejectOnly = true;
            //}
            //if (content.CanHide)
            //{
            //    content.hideOnly = true;
            //}
            //if (content.CanDelete)
            //{
            //    content.deleteOnly = true;
            //}

            return content;
        }

        public static bool CheckIfContenteXIS(string siteCode,string contentCode)
        {
            try
            {
                var content = new Content() { siteCode = siteCode, code = contentCode, version = contentCode.Substring(contentCode.Length - 2).ToString()  };

                return System.IO.Directory.Exists(content.folderPath);
            }
            catch (System.Exception)
            {

                return true;
            }
                    
          

           }

        public void Publish(string changes)
        {
            versions.Publish(this, changes);
            // Added by Kalam on 12/02/2020, fix to append the current content
            publishedOnly = false;
            summaryList.Publish(this);
        }

        public void RequestReview()
        {
            versions.RequestReview(this);

            summaryList.RequestReview(this);
        }

        public void AddRating(Rating rating)
        {
            int _countVotes = 0;
            int _ratingsValue = 0;

            ratingList.Add(rating, out _countVotes, out _ratingsValue);
            //out int countPublishedComments, out int countVotes, out int ratingValue

            countVotes = _countVotes;
            ratingsValue = _ratingsValue;

            Save(false);
        }

        public void AddComment(Comment comment)
        {
            int _countPublishedComments = 0;

            commentList.Add(comment, out _countPublishedComments);
            //out int countPublishedComments, out int countVotes, out int ratingValue

            countPublishedComments = _countPublishedComments;

            Save(false);
        }

        public void PublishComment(string guid)
        {
            commentList.Publish(guid);

            countPublishedComments++;

            Save(false);
        }
    }
}
