using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.@event
{
    public class SwitchTeamEvent : MatchEvent {
        [JsonProperty]
        public String fromTeamName {get; set;}
        [JsonProperty]
        public String toTeamName {get; set;}
    }
}
