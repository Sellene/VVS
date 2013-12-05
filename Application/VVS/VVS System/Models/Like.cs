using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public enum LikeType
    {
        LIKE,
        DISLIKE
    }

    public class Like
    {
        public User User { get; set; }

        public Video Video {get; set; }

        public LikeType LikeType { get; set; }

        public Like(User user, Video video, LikeType like)
        {
            User = user;
            Video = video;
            LikeType = like;
        }
    }
}