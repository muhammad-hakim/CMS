using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class RelationList
    {
        public List<Relation> relations { get; set; }

        internal static string filePath
        {
            get
            {
                return Config.ContentPath + "relations.json";
            }
        }

        static RelationList _relations;

        static void Load()
        {
            if (System.IO.File.Exists(filePath))
            {
                string text = System.IO.File.ReadAllText(filePath);

                _relations = Newtonsoft.Json.JsonConvert.DeserializeObject<RelationList>(text);
            }
            else
            {
                _relations = new RelationList() { relations = new List<Relation>() };
            }
        }

        public static List<Relation> GetRelations(string siteCode, string contentCode)
        {
            if (_relations == null) Load();

            return _relations != null ? _relations.relations.FindAll(x =>
                    x.fromSite == siteCode &&
                    x.fromContent == contentCode) : new List<Relation>();

            //return _relations.relations;
        }

        public static void AddContentRelation(string siteCode, string contentCode, List<Relation> contentRelations)
        {
            if (_relations == null) Load();

            //cleanup 
            foreach(var r in GetRelations(siteCode, contentCode))
            {
                if (contentRelations.FindIndex(x => x.guid == r.guid) < 0)
                {
                    _relations.relations.Remove(r);
                }
            }

            //add the relations
            foreach (var r in contentRelations)
            {
                // if the relation exists, then skip adding it
                //if(_relations.relations.FindIndex(x=>
                //    x.fromChapter == r.fromChapter &&
                //    x.fromArticle == r.fromArticle &&
                //    x.toChapter == r.toChapter &&
                //    x.toArticle == r.toArticle) >-1)
                //{
                //    r.updated = DateTime.Now;

                //    continue;
                //}

                var x = _relations.relations.Find(x => x.guid == r.guid);

                if (x != null)
                {
                    x.toSite = r.toSite;
                    x.toContent = r.toContent;
                    x.updated = DateTime.Now;
                }
                else
                {
                    r.guid = System.Guid.NewGuid().ToString();
                    r.createdBy = User.username;
                    r.created = DateTime.Now;
                    r.updated = DateTime.Now;

                    _relations.relations.Add(r);
                }
            }

            

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(_relations);

            System.IO.File.WriteAllText(filePath, output);
        }
    }
}
