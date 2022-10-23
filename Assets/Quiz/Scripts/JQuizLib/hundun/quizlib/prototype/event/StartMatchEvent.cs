using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.@event
{
    public class StartMatchEvent : MatchEvent {
        [JsonProperty]
        public List<TeamPrototype> teamPrototypes {get; set;}
        [JsonProperty]
        public List<String> questionIds {get; set;}
    }
}

