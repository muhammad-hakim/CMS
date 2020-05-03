using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class RatingList
    {
        public List<Rating> ratings { get; set; }
        internal string contentPath { get; set; }

        internal string filePath
        {
            get
            {
                return contentPath + "\\" + "_ratings.json";
            }
        }

        public static RatingList GetList(Content content)
        {
            RatingList contentRatings = new RatingList() { contentPath = content.folderPath };

            if (System.IO.File.Exists(contentRatings.filePath))
            {
                string text = System.IO.File.ReadAllText(contentRatings.filePath);

                contentRatings = JsonConvert.DeserializeObject<RatingList>(text);

                contentRatings.contentPath = content.folderPath;
            }

            contentRatings.ratings = contentRatings.ratings == null ? new List<Rating>() : contentRatings.ratings;

            return contentRatings;
        }

        public void Add(Rating newRating, out int countVotes, out int ratingValue)
        {
            newRating.guid = System.Guid.NewGuid().ToString();
            newRating.created = DateTime.Now;
            newRating.createdBy = CMS.User.username;

            Rating currentUserRating = ratings.Find(r => r.createdBy == User.username);

            if (currentUserRating != null)
            {
                currentUserRating.value = newRating.value;
            }
            else
            {
                ratings.Add(newRating);
            }

            Save();

            //calculate value
            int _countVotes = 0;
            int _ratingSum = 0;

            ratings.ForEach(r => {

                if (r.value > 0) { _countVotes++; _ratingSum += r.value; }

            });
            countVotes = _countVotes;
            ratingValue = (int)System.Math.Ceiling((double)_ratingSum / _countVotes);
            ratingValue = ratingValue > 0 ? ratingValue : 1;
        }

        void Save()
        {
            var output = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            System.IO.File.WriteAllText(filePath, output);
        }
    }
}
