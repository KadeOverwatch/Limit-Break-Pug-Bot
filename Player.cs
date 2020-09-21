using System;
using System.Collections.Generic;
using System.Text;

namespace Limit_Break_Pug_Bot
{
    public class Player
    {
        public Guid ID { get; set; }
        public string Battle_Tag { get; set; }
        public string Discord_Tag { get; set; }
        public int Player_Rank { get; set; }
        public string Player_Team { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Modified_On { get; set; }

    }
}
