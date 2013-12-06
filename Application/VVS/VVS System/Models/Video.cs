using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class Video
    {
        public int ID { get; set; }
        public String Name { get; set; }

        public String VideoPath { get; set; }

        public String PosterPath { get; set; }

        public User Owner { get; set; }

        public int Visualizations { get; set; }

        public bool IsPrivate { get; set; }

        public bool AllowComments { get; set; }

        public bool IsAdvertisement { get; set; }

        public Video()
        {
        }

        public Video(int id, String name, String videoPath, String posterPath, User owner, bool isPrivate, bool allowComments, bool isAdvert)
        {
            ID = id;
            Name = name;
            VideoPath = videoPath;
            PosterPath = posterPath;
            Owner = owner;
            Visualizations = 0;
            IsPrivate = isPrivate;
            AllowComments = allowComments;
            IsAdvertisement = isAdvert;
        }
    }
}