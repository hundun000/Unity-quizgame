using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.buff
{
    public class BuffPrototype {
        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public String description {get; set;}
        [JsonProperty]
        public int maxDuration {get; set;}
        [JsonProperty]
        public BuffStrategyType buffStrategyType;

        public BuffPrototype(string name, string description, int maxDuration, BuffStrategyType buffStrategyType)
        {
            this.name = name;
            this.description = description;
            this.maxDuration = maxDuration;
            this.buffStrategyType = buffStrategyType;
        }
    }
}


