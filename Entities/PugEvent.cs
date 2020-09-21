using System;
using System.Collections.Generic;
using System.Text;

namespace Limit_Break_Pug_Bot.Entities
{
    public class PugEvent
    {
        public Guid ID { get; set; }
        public DateTime Scheduled_Date { get; set; }
        public DateTime Created_On { get; set; }
        public string Discord_Message_ID { get; set; }
    }
}
