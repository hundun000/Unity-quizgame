using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.match
{
    public class MatchConfig {
        [JsonProperty]
        public MatchStrategyType matchStrategyType {get; set;}
        [JsonProperty]
        public List<String> teamNames {get; set;}
        [JsonProperty]
        public String questionPackageName {get; set;}
    }
}

