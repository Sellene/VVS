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

        public Video(int id, String name, String videoPath, String posterPath)
        {
            ID = id;
            Name = name;
            VideoPath = videoPath;
            PosterPath = posterPath;
        }
    }
}