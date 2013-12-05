using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class VideoModel
    {
        public Video Video { get; set; }

        public int TotalLikes { get; set; }

        public int TotalDislikes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }


        public VideoModel(Video v, int likes, int dislikes, IEnumerable<Comment> comment)
        {
            Video = v;
            TotalLikes = likes;
            TotalDislikes = dislikes;
            Comments = comment;
        }
    }
}