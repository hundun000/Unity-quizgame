using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.view.buff
{
    public class BuffRuntimeView {
        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public String description {get; set;}
        [JsonProperty]
        public int duration {get; set;}
    }
}

