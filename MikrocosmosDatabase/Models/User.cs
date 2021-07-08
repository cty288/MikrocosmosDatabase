using System;
using System.Collections;
using System.Collections.Generic;


namespace MikrocosmosDatabase
{
    public class User
    {
        public User() { }
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Playfabid { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime? LastLoginTime { get; set; }

    }
}
