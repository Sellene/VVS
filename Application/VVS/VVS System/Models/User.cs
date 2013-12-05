using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVS_System.Models
{
    public class User
    {
        public int ID { get; set; }
        public String Name { get; set; }

        public List<User> Subscribe { get; set; } // A subscribe B
        
        public List<User> Subscribed { get; set; } // B is subcribed por A

        public User(int id, String name)
        {
            ID = id;
            Name = name;
        }

        public void AddSubscrition(User u)
        {
            Subscribe.Add(u);
            u.Subscribed.Add(this);
        }



    }
}