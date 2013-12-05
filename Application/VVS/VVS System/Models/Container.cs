using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class Container
    {
        private Dictionary<int, Video> _videos;
        private Dictionary<int, User> _users;
        private List<Comment> _comments;
        private List<Like> _likes;

        public Container()
        {
            CreateUsers();
            CreateVideos();
            CreateComments();
            CreateLikes();
        }

        private void CreateLikes()
        {
            _likes = new List<Like>();
            _likes.Add(new Like(_users[1], _videos[1], LikeType.LIKE));
            _likes.Add(new Like(_users[3], _videos[6], LikeType.LIKE));
            _likes.Add(new Like(_users[3], _videos[8], LikeType.LIKE));
            _likes.Add(new Like(_users[4], _videos[1], LikeType.DISLIKE));
            _likes.Add(new Like(_users[4], _videos[6], LikeType.LIKE));
            _likes.Add(new Like(_users[4], _videos[8], LikeType.LIKE));
            _likes.Add(new Like(_users[3], _videos[1], LikeType.DISLIKE));
            _likes.Add(new Like(_users[0], _videos[6], LikeType.DISLIKE));
            _likes.Add(new Like(_users[0], _videos[8], LikeType.LIKE));

        }

        private void CreateComments()
        {
            _comments = new List<Comment>();
            _comments.Add(new Comment(_users[0], _videos[1], "Nice!", DateTime.Now));
            _comments.Add(new Comment(_users[3], _videos[1], "Cool", DateTime.Now.AddHours(-1)));
            _comments.Add(new Comment(_users[3], _videos[1], "=D", DateTime.Now.AddMinutes(-30)));
         
        }

        private void CreateVideos()
        {
            _videos = new Dictionary<int, Video>();
            _videos.Add(0, new Video(0, "Audi 2013 Commercial", "../Content/Videos/Audi_2013_Commercial.mp4", "../Content/Videos/Posters/Audi_2013_Commercial.png", _users[1], true, true));
            _videos.Add(1, new Video(1, "Coke 2012 Commercial", "../Content/Videos/Coke_2012_Commercial.mp4", "../Content/Videos/Posters/Coke_2012_Commercial.png", _users[0], false, false));
            _videos.Add(2, new Video(2, "Heineken Commercial", "../Content/Videos/Heineken_Commercial.mp4", "../Content/Videos/Posters/Heineken_Commercial.png", _users[3], false, true));
            _videos.Add(3, new Video(3, "Hyundai Veloster Commercial", "../Content/Videos/Hyundai_Veloster_Commercial.mp4", "../Content/Videos/Posters/Hyundai_Veloster_Commercial.png", _users[0], false, true));
            _videos.Add(4, new Video(4, "M&M's Commercial", "../Content/Videos/MMs_Commercial.mp4", "../Content/Videos/Posters/MMs_Commercial.png", _users[4], false, true));
            _videos.Add(5, new Video(5, "Nintendo 3DS Luigi's Mansion Commercial", "../Content/Videos/Nintendo_3DS_Luigi's_Mansion_Commercial.mp4", "../Content/Videos/Posters/Nintendo_3DS Luigi's_Mansion_Commercial.png", _users[4], false, true));
            _videos.Add(6, new Video(6, "Turkish Airlines Commercial", "../Content/Videos/Turkish_Airlines_Commercial.mp4", "../Content/Videos/Posters/Turkish_Airlines_Commercial.png", _users[1], false, false));
            _videos.Add(7, new Video(7, "Volkswagen 2012 Commercial", "../Content/Videos/Volkswagen_2012_Commercial.mp4", "../Content/Videos/Posters/Volkswagen_2012_Commercial.png", _users[3], true, false));
            _videos.Add(8, new Video(8, "VW Passat 2011 Commercial", "../Content/Videos/VW_Passat_2011_Commercial.mp4", "../Content/Videos/Posters/VW_Passat_2011_Commercial.mp4", _users[1], false, true));
        }

        private void CreateUsers()
        {
            _users = new Dictionary<int, User>();
            _users.Add(0, new User(0, "Sellene"));
            _users.Add(1, new User(1, "Jonnybravo"));
            _users.Add(2, new User(2, "Né Né"));
            _users.Add(3, new User(3, "Cebolas"));
            _users.Add(4, new User(4, "Samurai"));

        }


        /**********************************************************************************************************/
        /*** VIDEO UTIL *******************************************************************************************/
        /**********************************************************************************************************/

        private IEnumerable<Video> getRecommendedVideos(int size = 6)
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

        private IEnumerable<Video> getVideos(string search)
        {
            return _videos.Values.Where(v => v.Name.ToUpper().Contains(search.ToUpper()));
        }

        public string [] getVideosNames(string search)
        {
            return _videos.Values.Select(v => v.Name).Where(v => v.ToUpper().Contains(search.ToUpper())).ToArray();
        }

        /**********************************************************************************************************/
        /*** LIKES UTIL *******************************************************************************************/
        /**********************************************************************************************************/

        private int getTotalLikes(Video v)
        {
            return _likes.Where(l => l.Video.Equals(v) && l.LikeType == LikeType.LIKE).Count();
        }

        private int getTotalDislikes(Video v)
        {
            return _likes.Where(l => l.Video.Equals(v) && l.LikeType == LikeType.DISLIKE).Count();
        }


        /**********************************************************************************************************/
        /*** COMMENT UTIL *****************************************************************************************/
        /**********************************************************************************************************/

        private IEnumerable<Comment> getVideoComments(Video v)
        {
            return _comments.Where(c => c.Video.Equals(v)).OrderByDescending(c => c.Date);
        }

        private IEnumerable<Comment> getUserComments(User u)
        {
            return _comments.Where(c => c.User.Equals(u)).OrderByDescending(c => c.Date);
        }


        /**********************************************************************************************************/
        /*** VIDEO MODEL *******************************************************************************************/
        /**********************************************************************************************************/

        public IEnumerable<VideoModel> getVideosModel(String search)
        {
            IEnumerable<Video> vm = search == null ? getRecommendedVideos() : getVideos(search);
            List<VideoModel> list = new List<VideoModel>();

            foreach (Video v in vm)
            {
                list.Add(new VideoModel(v, getTotalLikes(v), getTotalDislikes(v), null));
            }

            return list;
        }

        public VideoModel getVideoModel(int video)
        {
            Video v = getVideo(video);
            //v.Visualizations++;
            return new VideoModel(v, getTotalLikes(v), getTotalDislikes(v), getVideoComments(v)); 
        }
    }
}