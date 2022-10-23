using System;
using System.Collections.Generic;
using Newtonsoft.Json;

    namespace hundun.quizlib.prototype.@event
{
    public class SkillResultEvent : MatchEvent {
        [JsonProperty]
        public String teamName {get; set;}
        [JsonProperty]
        public String roleName {get; set;}
        [JsonProperty]
        public String skillName {get; set;}
        [JsonProperty]
        public String skillDesc {get; set;}
        [JsonProperty]
        public int skillRemainTime {get; set;}
        [JsonProperty]
        public List<String> args {get; set;}
    }
}
