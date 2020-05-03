using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class CommentList
    {
        public List<Comment> comments { get; set; }
        internal string contentPath { get; set; }

        internal string filePath
        {
            get
            {
                return contentPath + "\\" + "_comments.json";
            }
        }

        public static CommentList GetList(Content content)
        {
            CommentList conentComments = new CommentList() { contentPath = content.folderPath};

            if (System.IO.File.Exists(conentComments.filePath))
            {
                string text = System.IO.File.ReadAllText(conentComments.filePath);

                conentComments = JsonConvert.DeserializeObject<CommentList>(text);

                conentComments.contentPath = content.folderPath;
            }

            conentComments.comments = conentComments.comments == null ? new List<Comment>() : conentComments.comments;

            return conentComments;
        }

        public void Add(Comment comment, out int countPublishedComments)
        {
            comment.guid = System.Guid.NewGuid().ToString();
            comment.created = DateTime.Now;
            comment.createdBy = CMS.User.username;

            comments.Add(comment);

            Save();

            //count value and comments
            int _countPublishedComments = 0;

            comments.ForEach(c => {

                if (c.published > DateTime.MinValue && !string.IsNullOrWhiteSpace(c.text)) _countPublishedComments++;

            });

            countPublishedComments = _countPublishedComments;
        }

        public void Publish(string guid)
        {
            Comment comment = comments.Find(r => r.guid == guid);

            comment.published = DateTime.Now;
            comment.publishedBy = User.username;

            Save();
        }

        void Save()
        {
            var output = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            System.IO.File.WriteAllText(filePath, output);
        }
    }
}
