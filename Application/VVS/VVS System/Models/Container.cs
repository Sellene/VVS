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
        private int _idxVideos;

        private static Container _container;

        private Container()
        {
            CreateUsers();
            CreateVideos();
            CreateComments();
            CreateLikes();
        }

        public static Container GetContainer()
        {
            if (_container == null)
                _container = new Container();

            return _container;
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
            _videos.Add(0, new Video(0, "Audi 2013 Commercial", "../Content/Videos/Audi_2013_Commercial.mp4", "../Content/Videos/Posters/Audi_2013_Commercial.png", _users[1], false, true, true));
            _videos.Add(1, new Video(1, "Coke 2012 Commercial", "../Content/Videos/Coke_2012_Commercial.mp4", "../Content/Videos/Posters/Coke_2012_Commercial.png", _users[0], false, true, false));
            _videos.Add(2, new Video(2, "Heineken Commercial", "../Content/Videos/Heineken_Commercial.mp4", "../Content/Videos/Posters/Heineken_Commercial.png", _users[3], false, true, false));
            _videos.Add(3, new Video(3, "Hyundai Veloster Commercial", "../Content/Videos/Hyundai_Veloster_Commercial.mp4", "../Content/Videos/Posters/Hyundai_Veloster_Commercial.png", _users[0], false, true, true));
            _videos.Add(4, new Video(4, "M&M's Commercial", "../Content/Videos/MMs_Commercial.mp4", "../Content/Videos/Posters/MMs_Commercial.png", _users[4], false, true, true));
            _videos.Add(5, new Video(5, "Nintendo 3DS Luigi's Mansion Commercial", "../Content/Videos/Nintendo_3DS_Luigi's_Mansion_Commercial.mp4", "../Content/Videos/Posters/Nintendo_3DS Luigi's_Mansion_Commercial.png", _users[4], false, true, false));
            _videos.Add(6, new Video(6, "Turkish Airlines Commercial", "../Content/Videos/Turkish_Airlines_Commercial.mp4", "../Content/Videos/Posters/Turkish_Airlines_Commercial.png", _users[1], false, false, false));
            _videos.Add(7, new Video(7, "Volkswagen 2012 Commercial", "../Content/Videos/Volkswagen_2012_Commercial.mp4", "../Content/Videos/Posters/Volkswagen_2012_Commercial.png", _users[3], false, false, true));
            _videos.Add(8, new Video(8, "VW Passat 2011 Commercial", "../Content/Videos/VW_Passat_2011_Commercial.mp4", "../Content/Videos/Posters/VW_Passat_2011_Commercial.png", _users[1], false, true, false));
            _idxVideos = 9;
        }

        private void CreateUsers()
        {
            _users = new Dictionary<int, User>();
            _users.Add(0, new User(0, "Sellene"));
            _users.Add(1, new User(1, "Jonnybravo"));
            _users.Add(2, new User(2, "Né Né"));
            _users.Add(3, new User(3, "Cebolas"));
            _users.Add(4, new User(4, "Samurai"));
            _users.Add(5, new User(5, "Dummy"));

            _users[5].UpdateSubscrition(_users[1]);

        }


        /**********************************************************************************************************/
        /*** VIDEO UTIL *******************************************************************************************/
        /**********************************************************************************************************/

        private IEnumerable<Video> GetRecommendedVideos(int size = 6)
        {
            Random r = new Random();
            List<Video> recommended = new List<Video>();

            while (size > 0)
            {
                int num = r.Next(_videos.Count());
                if (!recommended.Contains(_videos[num]) && !_videos[num].IsPrivate)
                {
                    recommended.Add(_videos[num]);
                    size--;
                }
                
            }

            return recommended;
        }

        public Video GetVideo(int id)
        {
            if(id < _videos.Count())
              return _videos[id];

            return null;
        }

        private IEnumerable<Video> GetVideos(string search)
        {
            return _videos.Values.Where(v => v.Name.ToUpper().Contains(search.ToUpper()) && !v.IsPrivate);
        }

        public string [] GetVideosNames(string search)
        {
            return _videos.Values.Select(v => v.Name).Where(v => v.ToUpper().Contains(search.ToUpper())).ToArray();
        }

        public void AddVideo(Video video)
        {
            video.ID = _idxVideos;
            _videos.Add(_idxVideos++, video);
        }

        
        /**********************************************************************************************************/
        /*** LIKES UTIL *******************************************************************************************/
        /**********************************************************************************************************/

        private int GetTotalLikes(Video v)
        {
            return _likes.Where(l => l.Video.Equals(v) && l.LikeType == LikeType.LIKE).Count();
        }

        private int GetTotalDislikes(Video v)
        {
            return _likes.Where(l => l.Video.Equals(v) && l.LikeType == LikeType.DISLIKE).Count();
        }

        public String UpdateLikes(int video, int user, bool isLike)
        {
            Like l = new Like(_users[user], _videos[video], !isLike?LikeType.LIKE:LikeType.DISLIKE);
            if (_likes.Contains(l))
                _likes.Remove(l);
                
            _likes.Add(new Like(_users[user], _videos[video], isLike?LikeType.LIKE:LikeType.DISLIKE));
            
            return GetTotalLikes(_videos[video]) + ";" + GetTotalDislikes(_videos[video]);
        }


        /**********************************************************************************************************/
        /*** COMMENT UTIL *****************************************************************************************/
        /**********************************************************************************************************/

        private IEnumerable<Comment> GetVideoComments(Video v)
        {
            return _comments.Where(c => c.Video.Equals(v)).OrderByDescending(c => c.Date);
        }

        private IEnumerable<Comment> GetUserComments(User u)
        {
            return _comments.Where(c => c.User.Equals(u)).OrderByDescending(c => c.Date);
        }

        public Comment AddComment(int video, int user, String comment)
        {
            Comment c = new Comment(_users[user], _videos[video], comment, DateTime.Now);
            _comments.Add(c);
            return c;
        }

        /**********************************************************************************************************/
        /*** USER UTILS *******************************************************************************************/
        /**********************************************************************************************************/

        public void Subscribe(int channel, int viewer)
        {
            _users[viewer].UpdateSubscrition(_users[channel]);
        }

        public String IsSubscribed(int video, int viewer)
        {
            return _videos[video].Owner.Subscribed.Contains(_users[viewer])?"Unsubscribe":"Subscribe";
        }

        public User GetUser(int userId)
        {
            return _users[userId];
        }

        public void AddVideoToFavourites(int user, int video)
        {
            _users[user].Favourites.Add(_videos[video]);
        }
        /**********************************************************************************************************/
        /*** VIDEO MODEL *******************************************************************************************/
        /**********************************************************************************************************/

        public IEnumerable<VideoModel> GetVideosModel(String search)
        {
            IEnumerable<Video> vm = search == null ? GetRecommendedVideos() : GetVideos(search);
            List<VideoModel> list = new List<VideoModel>();

            foreach (Video v in vm)
            {
                list.Add(new VideoModel(v, GetTotalLikes(v), GetTotalDislikes(v), null, ""));
            }

            return list;
        }

        public VideoModel GetVideoModel(int video, int viewer)
        {
            Video v = GetVideo(video);
            return new VideoModel(v, GetTotalLikes(v), GetTotalDislikes(v), GetVideoComments(v), IsSubscribed(video, viewer)); 
        }

        public VideoModel GetAdvertisement(int video)
        {
            Random r = new Random();

            while (true)
            {
                int num = r.Next(_videos.Count());
                if (_videos[num].IsAdvertisement)
                {
                    VideoModel vm = new VideoModel(_videos[num], -1, -1, null, null);
                    vm.VideoToShow = video;
                    vm.IsAdvertisement = true;
                    return vm;
                }

            }
        }
    }
}