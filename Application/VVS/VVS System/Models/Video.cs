﻿using System;
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

        //public IEnumerable<String> Tags { get; set; }

        public User Owner { get; set; }

        public int Visualizations { get; set; }

        public bool IsPrivate { get; set; }

        public bool AllowComments { get; set; }

        public Video(int id, String name, String videoPath, String posterPath, User owner, bool isPrivate, bool allowComments)
        {
            ID = id;
            Name = name;
            VideoPath = videoPath;
            PosterPath = posterPath;
            Owner = owner;
            Visualizations = 0;
            IsPrivate = isPrivate;
            AllowComments = allowComments;
        }

        public void AddVisualization()
        {
            Visualizations++;
        }
    }
}