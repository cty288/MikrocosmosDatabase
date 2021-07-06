using System;
using System.Collections;
using System.Collections.Generic;


namespace MikrocosmosDatabase
{
    public class User
    {
        public User() { }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Playfabid { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
