using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class VideoContainer
    {
        private Dictionary<int, Video> _videos;

        public VideoContainer()
        {
            _videos = new Dictionary<int,Video>();
            _videos.Add(0, new Video(0,"Audi 2013 Commercial", "../Content/Videos/Audi_2013_Commercial.mp4", "../Content/Videos/Posters/Audi_2013_Commercial.png"));
            _videos.Add(1, new Video(1, "Coke 2012 Commercial", "../Content/Videos/Coke_2012_Commercial.mp4", "../Content/Videos/Posters/Coke_2012_Commercial.png"));
            _videos.Add(2, new Video(2, "Heineken Commercial", "../Content/Videos/Heineken_Commercial.mp4", "../Content/Videos/Posters/Heineken_Commercial.png"));
            _videos.Add(3, new Video(3, "Hyundai Veloster Commercial", "../Content/Videos/Hyundai_Veloster_Commercial.mp4", "../Content/Videos/Posters/Hyundai_Veloster_Commercial.png"));
            _videos.Add(4, new Video(4, "M&M's Commercial", "../Content/Videos/MMs_Commercial.mp4", "../Content/Videos/Posters/MMs_Commercial.png"));
            _videos.Add(5, new Video(5, "Nintendo 3DS Luigi's Mansion Commercial", "../Content/Videos/Nintendo_3DS_Luigi's_Mansion_Commercial.mp4", "../Content/Videos/Posters/Nintendo_3DS Luigi's_Mansion_Commercial.png"));
            _videos.Add(6, new Video(6, "Turkish Airlines Commercial", "../Content/Videos/Turkish_Airlines_Commercial.mp4", "../Content/Videos/Posters/Turkish_Airlines_Commercial.png"));
            _videos.Add(7, new Video(7, "Volkswagen 2012 Commercial", "../Content/Videos/Volkswagen_2012_Commercial.mp4", "../Content/Videos/Posters/Volkswagen_2012_Commercial.png"));
            _videos.Add(8, new Video(8, "VW Passat 2011 Commercial", "../Content/Videos/VW_Passat_2011_Commercial.mp4", "../Content/Videos/Posters/VW_Passat_2011_Commercial.mp4"));
        }

        public IEnumerable<Video> getRecommended(int size = 4)
        {
            Random r = new Random();
            List<Video> recommended = new List<Video>();

            while (size > 0)
            {
                int num = r.Next(_videos.Count()-1);
                if (!recommended.Contains(_videos[num]))
                {
                    recommended.Add(_videos[num]);
                    size--;
                }
                
            }

            return recommended;
        }

        public Video getVideo(int id)
        {
            if(id < _videos.Count())
              return _videos[id];

            return null;
        }

        public IEnumerable<Video> getVideos(string search)
        {
            return _videos.Values.Where(v => v.Name.ToUpper().Contains(search.ToUpper()));
        }
    }
}