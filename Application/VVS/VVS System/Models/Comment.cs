using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class Comment
    {
        public User User { get; set; }

        public Video Video {get; set; }

        public String Text { get; set; }

        public DateTime Date { get; set; }

        public Comment(User user, Video video, String comment, DateTime date)
        {
            User = user;
            Video = video;
            Text = comment;
            Date = date;
        }
    }
}