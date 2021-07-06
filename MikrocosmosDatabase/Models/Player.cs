using System;
using System.Collections.Generic;
using System.Text;

namespace MikrocosmosDatabase
{
    public class Player
    {
        public int Id { get; set; }
        public User Users { get; set; }
        public string DisplayName { get; set; }
        public string JoinedMatchid { get; set; }
    }
}
