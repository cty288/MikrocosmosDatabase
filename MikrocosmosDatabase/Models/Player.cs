using System;
using System.Collections.Generic;
using System.Text;

namespace MikrocosmosDatabase
{
    public class Player
    {
        public virtual int Id { get; set; }
        public virtual User Users { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string JoinedMatchid { get; set; }
    }
}
