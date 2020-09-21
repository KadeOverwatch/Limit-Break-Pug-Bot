using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Limit_Break_Pug_Bot
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
       
        [JsonProperty("CommandPrefix")]
        public string Prefix { get; private set; }
        
        [JsonProperty("appId")]
        public string AppId { get; private set; }

        [JsonProperty("appSecret")]
        public string AppSecret { get; private set; }

    }
}