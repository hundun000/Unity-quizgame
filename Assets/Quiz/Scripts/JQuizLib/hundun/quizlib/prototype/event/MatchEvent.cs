using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.@event
{
    public abstract class MatchEvent {
        [JsonProperty]
        public EventType type {get; set;}
    }
}
