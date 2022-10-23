using hundun.quizlib.prototype.match;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

    namespace hundun.quizlib.prototype.@event
{

    public class AnswerResultEvent : MatchEvent {
        [JsonProperty]
        public AnswerType result {get; set;}
        [JsonProperty]
        public int addScore {get; set;}
        [JsonProperty]
        public String addScoreTeamName {get; set;}
    }

}

