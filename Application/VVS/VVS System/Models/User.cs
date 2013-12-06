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
            Subscribed = new List<User>();
            Subscribe = new List<User>();
        }

        public void UpdateSubscrition(User u)
        {
            if (Subscribe.Contains(u))
            {
                Subscribe.Remove(u);
                u.Subscribed.Remove(this);
            }
            else
            {
                Subscribe.Add(u);
                u.Subscribed.Add(this);
            } 
        }
    }
}