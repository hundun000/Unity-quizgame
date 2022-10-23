using System;
using System.Collections.Generic;
using Newtonsoft.Json;

    namespace hundun.quizlib.prototype.@event
{
    public class SwitchQuestionEvent : MatchEvent {
        [JsonProperty]
        public int time {get; set;}
    }
}