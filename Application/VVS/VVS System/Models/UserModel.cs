using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class UserModel
    {
        public User User { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public List<Video> Videos { get; set; }

        public String Subscription { get; set; }

        public UserModel(User user, IEnumerable<Comment> comment, List<Video> videos, String subscription)
        {
            User = user;
            Comments = comment;
            Videos = videos;
            Subscription = subscription;
        }



    }
}